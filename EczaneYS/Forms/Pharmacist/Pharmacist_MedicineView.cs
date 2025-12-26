using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using EczaneYS.Data;
using EczaneYS.Services;

namespace EczaneYS.Forms.Pharmacist
{
    public partial class Pharmacist_MedicineView : Form
    {
        public Pharmacist_MedicineView()
        {
            InitializeComponent();
        }

        // =========================
        // FORM LOAD
        // =========================
        private void Pharmacist_MedicineView_Load(object sender, EventArgs e)
        {
            // 🔐 Görüntüleme yetkisi yoksa form açılmaz
            if (!PermissionService.HasPermission("MEDICINE_VIEW"))
            {
                MessageBox.Show("İlaçları görüntüleme yetkiniz yok.");
                this.Close();
                return;
            }

            ApplyPermissions();
            LoadMedicines();
        }

        // =========================
        // 🔐 YETKİ KONTROLLERİ
        // =========================
        private void ApplyPermissions()
        {
            btnAdd.Visible = PermissionService.HasPermission("MEDICINE_ADD");
            btnUpdate.Visible = PermissionService.HasPermission("MEDICINE_UPDATE");
            btnPriceUpdate.Visible = PermissionService.HasPermission("MEDICINE_PRICE_UPDATE");
            btnDelete.Visible = PermissionService.HasPermission("MEDICINE_DELETE");
        }

        // =========================
        // 📋 İLAÇLARI YÜKLE
        // =========================
        private void LoadMedicines()
        {
            DataTable dt = DBHelper.GetDataTable(@"
                SELECT 
                    i.ilac_id,
                    i.ad AS ""İlaç"",
                    i.barkod AS ""Barkod"",
                    k.kategori_adi AS ""Kategori"",
                    i.fiyat AS ""Fiyat (₺)"",
                    i.stok AS ""Stok"",
                    i.receteli_mi AS ""Reçeteli"",
                    t.firma_adi AS ""Tedarikçi""
                FROM ilac i
                LEFT JOIN kategori k ON k.kategori_id = i.kategori_id
                LEFT JOIN tedarikci t ON t.tedarikci_id = i.tedarikci_id
                WHERE i.aktif = TRUE
                ORDER BY i.ad");

            dgvMedicines.DataSource = dt;

            if (dgvMedicines.Columns.Contains("ilac_id"))
                dgvMedicines.Columns["ilac_id"].Visible = false;

            HighlightPrescriptionMedicines();

            dgvMedicines.AllowUserToAddRows = false;
        }

        // =========================
        // 🎨 REÇETELİ İLAÇLARI RENKLENDİR
        // =========================
        private void HighlightPrescriptionMedicines()
        {
            foreach (DataGridViewRow row in dgvMedicines.Rows)
            {
                if (row.Cells["Reçeteli"].Value != null &&
                    Convert.ToBoolean(row.Cells["Reçeteli"].Value))
                {
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                }
            }
        }

        // =========================
        // ➕ İLAÇ EKLE
        // =========================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!PermissionService.HasPermission("MEDICINE_ADD"))
            {
                MessageBox.Show("İlaç ekleme yetkiniz yok.");
                return;
            }

            using (var form = new Pharmacist_MedicineAdd())
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadMedicines();
            }
        }

        // =========================
        // ✏️ İLAÇ GÜNCELLE
        // =========================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!PermissionService.HasPermission("MEDICINE_UPDATE"))
            {
                MessageBox.Show("İlaç güncelleme yetkiniz yok.");
                return;
            }

            if (dgvMedicines.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir ilaç seçiniz.");
                return;
            }

            int ilacId = Convert.ToInt32(
                dgvMedicines.SelectedRows[0].Cells["ilac_id"].Value
            );

            using (var form = new Pharmacist_MedicineUpdate(ilacId))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadMedicines();
            }
        }

        // =========================
        // 💰 FİYAT GÜNCELLE
        // =========================
        private void btnPriceUpdate_Click(object sender, EventArgs e)
        {
            if (!PermissionService.HasPermission("MEDICINE_PRICE_UPDATE"))
            {
                MessageBox.Show("Fiyat güncelleme yetkiniz yok.");
                return;
            }

            if (dgvMedicines.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir ilaç seçiniz.");
                return;
            }

            int ilacId = Convert.ToInt32(
                dgvMedicines.SelectedRows[0].Cells["ilac_id"].Value
            );

            string ilacAdi =
                dgvMedicines.SelectedRows[0].Cells["İlaç"].Value.ToString();

            using (Form fiyatForm = new Form())
            {
                fiyatForm.Text = "Fiyat Güncelle";
                fiyatForm.StartPosition = FormStartPosition.CenterParent;
                fiyatForm.Size = new Size(300, 150);
                fiyatForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                fiyatForm.MaximizeBox = false;
                fiyatForm.MinimizeBox = false;

                Label lbl = new Label
                {
                    Text = $"{ilacAdi} için yeni fiyat:",
                    Location = new Point(10, 15),
                    AutoSize = true
                };

                TextBox txtFiyat = new TextBox
                {
                    Location = new Point(10, 40),
                    Width = 260
                };

                Button btnOk = new Button
                {
                    Text = "Kaydet",
                    Location = new Point(100, 75),
                    DialogResult = DialogResult.OK
                };

                fiyatForm.Controls.Add(lbl);
                fiyatForm.Controls.Add(txtFiyat);
                fiyatForm.Controls.Add(btnOk);
                fiyatForm.AcceptButton = btnOk;

                if (fiyatForm.ShowDialog() != DialogResult.OK)
                    return;

                if (!decimal.TryParse(txtFiyat.Text, out decimal yeniFiyat) || yeniFiyat <= 0)
                {
                    MessageBox.Show("Geçerli bir fiyat giriniz.");
                    return;
                }

                DBHelper.ExecuteNonQuery(@"
                    UPDATE ilac
                    SET fiyat = @f
                    WHERE ilac_id = @id",
                    "@f", yeniFiyat,
                    "@id", ilacId
                );

                MessageBox.Show("Fiyat güncellendi.");
                LoadMedicines();
            }
        }

        // =========================
        // 🗑️ İLAÇ SİL (HARD DELETE)
        // =========================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!PermissionService.HasPermission("MEDICINE_DELETE"))
            {
                MessageBox.Show("İlaç silme yetkiniz yok.");
                return;
            }

            if (dgvMedicines.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek ilacı seçiniz.");
                return;
            }

            int ilacId = Convert.ToInt32(
                dgvMedicines.SelectedRows[0].Cells["ilac_id"].Value
            );

            string ilacAdi =
                dgvMedicines.SelectedRows[0].Cells["İlaç"].Value.ToString();

            DialogResult dr = MessageBox.Show(
                $"{ilacAdi} adlı ilaç silinsin mi?\n\nBu işlem geri alınamaz!",
                "Silme Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (dr != DialogResult.Yes)
                return;

            DBHelper.ExecuteNonQuery(@"
        DELETE FROM ilac
        WHERE ilac_id = @id",
                "@id", ilacId
            );

            MessageBox.Show("İlaç silindi.");
            LoadMedicines(); // 👈 Grid yenilenir
        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvMedicines_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
