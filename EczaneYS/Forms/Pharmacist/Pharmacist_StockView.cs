using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;
using EczaneYS.Data;
using EczaneYS.Services;

namespace EczaneYS.Forms.Pharmacist
{
    public partial class Pharmacist_StockView : Form
    {
        private DataTable _stockTable;
        private readonly int _currentUserId;

        public Pharmacist_StockView() : this(AuthContext.UserId) { }

        public Pharmacist_StockView(int currentUserId)
        {
            InitializeComponent();
            _currentUserId = currentUserId;
        }

        // ===============================
        // FORM LOAD
        // ===============================
        private void Pharmacist_StockView_Load(object sender, EventArgs e)
        {
            // 🔐 STOK GÖRÜNTÜLEME YETKİSİ
            if (!PermissionService.HasPermission("STOCK_VIEW"))
            {
                MessageBox.Show(
                    "Stokları görüntüleme yetkiniz yok.",
                    "Yetkisiz Erişim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                Close();
                return;
            }

            ApplyPermissions();
            LoadStock();
        }

        // ===============================
        // YETKİYE GÖRE BUTONLAR
        // ===============================
        private void ApplyPermissions()
        {
            btnStockIn.Visible = PermissionService.HasPermission("STOCK_IN");
            btnStockOut.Visible = PermissionService.HasPermission("STOCK_OUT");
        }

        // ===============================
        // STOK YÜKLE
        // ===============================
        private void LoadStock()
        {
            _stockTable = DBHelper.GetDataTable(@"
                SELECT
                    i.ilac_id,
                    i.ad AS ""İlaç"",
                    i.barkod AS ""Barkod"",
                    i.stok AS ""Stok"",
                    i.min_stok AS ""Min Stok"",
                    k.kategori_adi AS ""Kategori"",
                    i.receteli_mi AS ""Reçeteli""
                FROM ilac i
                LEFT JOIN kategori k ON k.kategori_id = i.kategori_id
                WHERE i.aktif = TRUE
                ORDER BY i.ad
            ");

            dgvStock.DataSource = _stockTable;

            if (dgvStock.Columns.Contains("ilac_id"))
                dgvStock.Columns["ilac_id"].Visible = false;

            EnsureExtraColumns();

            dgvStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStock.MultiSelect = false;

            HighlightLowStock();
        }

        // ===============================
        // EK KOLONLAR
        // ===============================
        private void EnsureExtraColumns()
        {
            if (dgvStock.Columns["warn"] == null)
            {
                dgvStock.Columns.Insert(0, new DataGridViewTextBoxColumn
                {
                    Name = "warn",
                    HeaderText = "⚠",
                    Width = 45,
                    ReadOnly = true
                });
            }

            if (dgvStock.Columns["bar"] == null)
            {
                dgvStock.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "bar",
                    HeaderText = "Stok Seviyesi",
                    ReadOnly = true
                });
            }

            dgvStock.CellFormatting -= dgvStock_CellFormatting;
            dgvStock.CellFormatting += dgvStock_CellFormatting;
        }

        // ===============================
        // DÜŞÜK STOK RENK
        // ===============================
        private void HighlightLowStock()
        {
            if (!PermissionService.HasPermission("LOW_STOCK_VIEW"))
                return;

            foreach (DataGridViewRow row in dgvStock.Rows)
            {
                if (row.IsNewRow) continue;

                int stok = Convert.ToInt32(row.Cells["Stok"].Value);
                int minStok = Convert.ToInt32(row.Cells["Min Stok"].Value);

                if (stok <= minStok)
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                    row.Cells["warn"].Value = "⚠";
                }
                else
                {
                    row.Cells["warn"].Value = "";
                }
            }
        }

        // ===============================
        // CELL FORMATTING (BAR)
        // ===============================
        private void dgvStock_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvStock.Rows[e.RowIndex].IsNewRow)
                return;

            if (dgvStock.Columns[e.ColumnIndex].Name == "bar")
            {
                int stok = Convert.ToInt32(dgvStock.Rows[e.RowIndex].Cells["Stok"].Value);
                int min = Convert.ToInt32(dgvStock.Rows[e.RowIndex].Cells["Min Stok"].Value);

                int denom = Math.Max(min * 2, 1);
                int percent = Math.Max(0, Math.Min(100, (stok * 100) / denom));

                int blocks = percent / 10;
                string bar = new string('▮', blocks).PadRight(10, '▯');

                e.Value = $"{bar}  {percent}%";
                e.FormattingApplied = true;
            }
        }

        // ===============================
        // ARAMA
        // ===============================
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (_stockTable == null) return;

            string text = (txtSearch.Text ?? "").Replace("'", "''").Trim();

            _stockTable.DefaultView.RowFilter = string.IsNullOrEmpty(text)
                ? ""
                : $"[İlaç] LIKE '%{text}%' OR [Barkod] LIKE '%{text}%'";
        }

        // ===============================
        // SEÇİLİ İLAÇ ID
        // ===============================
        private int? GetSelectedIlacId()
        {
            if (dgvStock.SelectedRows.Count == 0) return null;
            return Convert.ToInt32(dgvStock.SelectedRows[0].Cells["ilac_id"].Value);
        }

        // ===============================
        // STOK GİRİŞ
        // ===============================
        private void btnStockIn_Click(object sender, EventArgs e)
        {
            if (!PermissionService.HasPermission("STOCK_IN"))
            {
                MessageBox.Show("Stok giriş yetkiniz yok.");
                return;
            }

            int? ilacId = GetSelectedIlacId();
            if (ilacId == null)
            {
                MessageBox.Show("Lütfen bir ilaç seçiniz.");
                return;
            }

            if (!PromptAmount("Stok Girişi", "Giriş miktarını giriniz:", out int miktar))
                return;

            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    using (var tx = conn.BeginTransaction())
                    {
                        using (var updCmd = new NpgsqlCommand(
                            "UPDATE ilac SET stok = stok + @m WHERE ilac_id = @id",
                            conn, tx))
                        {
                            updCmd.Parameters.AddWithValue("@m", miktar);
                            updCmd.Parameters.AddWithValue("@id", ilacId.Value);
                            updCmd.ExecuteNonQuery();
                        }

                        using (var movCmd = new NpgsqlCommand(@"
                    INSERT INTO stok_hareket (ilac_id, miktar, hareket_tipi, aciklama)
                    VALUES (@id, @m, 'GIRIS', @a)", conn, tx))
                        {
                            movCmd.Parameters.AddWithValue("@id", ilacId.Value);
                            movCmd.Parameters.AddWithValue("@m", miktar);
                            movCmd.Parameters.AddWithValue("@a", "Eczacı stok girişi");
                            movCmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                    }
                }

                MessageBox.Show("Stok girişi yapıldı ✅");
                LoadStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Stok girişi sırasında hata:\n" + ex.Message);
            }
        }

        // ===============================
        // STOK ÇIKIŞ
        // ===============================
        private void btnStockOut_Click(object sender, EventArgs e)
        {
            if (!PermissionService.HasPermission("STOCK_OUT"))
            {
                MessageBox.Show("Stok çıkış yetkiniz yok.");
                return;
            }

            int? ilacId = GetSelectedIlacId();
            if (ilacId == null)
            {
                MessageBox.Show("Lütfen bir ilaç seçiniz.");
                return;
            }

            if (!PromptAmount("Stok Çıkışı", "Çıkış miktarını giriniz:", out int miktar))
                return;

            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    using (var tx = conn.BeginTransaction())
                    {
                        int stok;
                        using (var checkCmd = new NpgsqlCommand(
                            "SELECT stok FROM ilac WHERE ilac_id=@id FOR UPDATE",
                            conn, tx))
                        {
                            checkCmd.Parameters.AddWithValue("@id", ilacId.Value);
                            stok = Convert.ToInt32(checkCmd.ExecuteScalar());
                        }

                        if (miktar > stok)
                        {
                            tx.Rollback();
                            MessageBox.Show("Yetersiz stok!");
                            return;
                        }

                        using (var updCmd = new NpgsqlCommand(
                            "UPDATE ilac SET stok = stok - @m WHERE ilac_id = @id",
                            conn, tx))
                        {
                            updCmd.Parameters.AddWithValue("@m", miktar);
                            updCmd.Parameters.AddWithValue("@id", ilacId.Value);
                            updCmd.ExecuteNonQuery();
                        }

                        using (var movCmd = new NpgsqlCommand(@"
                            INSERT INTO stok_hareket (ilac_id, miktar, hareket_tipi, aciklama)
                            VALUES (@id, @m, 'CIKIS', @a)", conn, tx))
                        {
                            movCmd.Parameters.AddWithValue("@id", ilacId.Value);
                            movCmd.Parameters.AddWithValue("@m", -miktar);
                            movCmd.Parameters.AddWithValue("@a", "Eczacı stok çıkışı");
                            movCmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                    }
                }

                MessageBox.Show("Stok çıkışı yapıldı ✅");
                LoadStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Stok çıkışı sırasında hata:\n" + ex.Message);
            }
        }

        // ===============================
        // MİKTAR GİRİŞ MODALI
        // ===============================
        private bool PromptAmount(string title, string label, out int amount)
        {
            amount = 0;

            using (Form f = new Form())
            {
                f.Text = title;
                f.Size = new Size(280, 150);
                f.StartPosition = FormStartPosition.CenterParent;
                f.FormBorderStyle = FormBorderStyle.FixedDialog;
                f.MaximizeBox = false;
                f.MinimizeBox = false;

                Label lbl = new Label
                {
                    Text = label,
                    Location = new Point(10, 15),
                    AutoSize = true
                };

                TextBox txt = new TextBox
                {
                    Location = new Point(10, 40),
                    Width = 240
                };

                Button ok = new Button
                {
                    Text = "Kaydet",
                    Location = new Point(90, 75),
                    DialogResult = DialogResult.OK
                };

                f.Controls.Add(lbl);
                f.Controls.Add(txt);
                f.Controls.Add(ok);
                f.AcceptButton = ok;

                if (f.ShowDialog() != DialogResult.OK)
                    return false;

                if (!int.TryParse(txt.Text, out amount) || amount <= 0)
                {
                    MessageBox.Show("Geçerli bir miktar giriniz.");
                    return false;
                }

                return true;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStock();
        }

        private void dgvStock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
