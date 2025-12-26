using System;
using System.Data;
using System.Windows.Forms;
using EczaneYS.Data;
using EczaneYS.Services;
using Npgsql;

namespace EczaneYS.Forms.Pharmacist
{
    public partial class Pharmacist_SaleReturn : Form
    {
        public Pharmacist_SaleReturn()
        {
            InitializeComponent();
        }

        // ===============================
        // FORM LOAD
        // ===============================
        private void Pharmacist_SaleReturn_Load(object sender, EventArgs e)
        {
            if (!PermissionService.HasPermission("SALE_RETURN"))
            {
                MessageBox.Show(
                    "Satış iade yetkiniz yok.",
                    "Yetkisiz Erişim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                this.Close();
                return;
            }

            SetupGrid();
            LoadSales();
        }

        // ===============================
        // GRID AYARLARI
        // ===============================
        private void SetupGrid()
        {
            dgvSales.ReadOnly = true;
            dgvSales.AllowUserToAddRows = false;
            dgvSales.AllowUserToDeleteRows = false;
            dgvSales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSales.RowHeadersVisible = false;
        }

        // ===============================
        // SATIŞLARI YÜKLE (İADE EDİLMEMİŞ)
        // ===============================
        private void LoadSales()
        {
            dgvSales.DataSource = DBHelper.GetDataTable(@"
                SELECT
                    s.satis_id AS ""Satış No"",
                    s.tarih AS ""Tarih"",
                    COALESCE(m.ad || ' ' || m.soyad, '-') AS ""Müşteri"",
                    s.toplam_fiyat AS ""Toplam (₺)""
                FROM satis s
                LEFT JOIN musteri m ON m.musteri_id = s.musteri_id
                WHERE s.is_returned = FALSE
                ORDER BY s.tarih DESC
            ");

            dgvSales.Columns["Toplam (₺)"].DefaultCellStyle.Format = "N2";
        }

        // ===============================
        // İADE ET
        // ===============================
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir satış seçiniz.");
                return;
            }

            int satisId = Convert.ToInt32(dgvSales.SelectedRows[0].Cells["Satış No"].Value);

            if (MessageBox.Show(
                "Bu satışı iade etmek istediğinize emin misiniz?",
                "İade Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open(); // ✅ KRİTİK

                    using (var tx = conn.BeginTransaction())
                    {
                        // 1) Satış satırını kilitle + iade kontrolü
                        bool alreadyReturned;
                        using (var checkCmd = new NpgsqlCommand(@"
                    SELECT is_returned
                    FROM satis
                    WHERE satis_id = @id
                    FOR UPDATE", conn, tx))
                        {
                            checkCmd.Parameters.AddWithValue("@id", satisId);

                            object res = checkCmd.ExecuteScalar();
                            if (res == null)
                            {
                                tx.Rollback();
                                MessageBox.Show("Satış bulunamadı.");
                                return;
                            }

                            alreadyReturned = Convert.ToBoolean(res);
                        }

                        if (alreadyReturned)
                        {
                            tx.Rollback();
                            MessageBox.Show("Bu satış zaten iade edilmiş.");
                            return;
                        }

                        // 2) Satış detaylarını çek
                        DataTable detaylar = new DataTable();
                        using (var detCmd = new NpgsqlCommand(@"
                    SELECT ilac_id, adet
                    FROM satis_detay
                    WHERE satis_id = @id", conn, tx))
                        {
                            detCmd.Parameters.AddWithValue("@id", satisId);
                            using (var da = new NpgsqlDataAdapter(detCmd))
                                da.Fill(detaylar);
                        }

                        // 3) Stokları geri ekle
                        foreach (DataRow r in detaylar.Rows)
                        {
                            int ilacId = Convert.ToInt32(r["ilac_id"]);
                            int adet = Convert.ToInt32(r["adet"]);

                            using (var updStock = new NpgsqlCommand(@"
                        UPDATE ilac
                        SET stok = stok + @a
                        WHERE ilac_id = @i", conn, tx))
                            {
                                updStock.Parameters.AddWithValue("@a", adet);
                                updStock.Parameters.AddWithValue("@i", ilacId);
                                updStock.ExecuteNonQuery();
                            }

                            // (Opsiyonel ama önerilir) stok_hareket kaydı
                            using (var movCmd = new NpgsqlCommand(@"
                        INSERT INTO stok_hareket (ilac_id, miktar, hareket_tipi, aciklama)
                        VALUES (@i, @m, 'IADE', @a)", conn, tx))
                            {
                                movCmd.Parameters.AddWithValue("@i", ilacId);
                                movCmd.Parameters.AddWithValue("@m", adet);
                                movCmd.Parameters.AddWithValue("@a", $"Satış iadesi (Satış No: {satisId})");
                                movCmd.ExecuteNonQuery();
                            }
                        }

                        // 4) Satışı iade edildi yap
                        using (var updSale = new NpgsqlCommand(@"
                    UPDATE satis
                    SET is_returned = TRUE
                    WHERE satis_id = @id", conn, tx))
                        {
                            updSale.Parameters.AddWithValue("@id", satisId);
                            updSale.ExecuteNonQuery();
                        }

                        tx.Commit();
                    }
                }

                MessageBox.Show("Satış başarıyla iade edildi ✅");
                LoadSales();
            }
            catch (PostgresException pex)
            {
                MessageBox.Show(
                    "İade sırasında veritabanı hatası:\n" +
                    pex.MessageText +
                    (string.IsNullOrEmpty(pex.Detail) ? "" : "\nDetay: " + pex.Detail),
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "İade sırasında hata oluştu:\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        // ===============================
        // KAPAT
        // ===============================
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvSales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
