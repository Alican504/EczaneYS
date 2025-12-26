using EczaneYS.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace EczaneYS.Forms.Admin.Controls
{
    public partial class Admin_AddMedicine : UserControl
    {
        public Admin_AddMedicine()
        {
            InitializeComponent();
            LoadCategories();
            LoadSuppliers();
        }

        // =========================
        // LOAD CATEGORIES
        // =========================
        private void LoadCategories()
        {
            var dt = DBHelper.GetDataTable(@"
        SELECT 
            kategori_id, 
            kategori_adi AS ad
        FROM kategori
        WHERE aktif = true
        ORDER BY kategori_adi
    ");

            cmbKategori.DataSource = dt;
            cmbKategori.DisplayMember = "ad";
            cmbKategori.ValueMember = "kategori_id";
            cmbKategori.SelectedIndex = -1;
        }


        // =========================
        // LOAD SUPPLIERS
        // =========================
        private void LoadSuppliers()
        {
            var dt = DBHelper.GetDataTable(@"
        SELECT 
            tedarikci_id, 
            firma_adi AS ad
        FROM tedarikci
        WHERE aktif = true
        ORDER BY firma_adi
    ");

            cmbTedarikci.DataSource = dt;
            cmbTedarikci.DisplayMember = "ad";
            cmbTedarikci.ValueMember = "tedarikci_id";
            cmbTedarikci.SelectedIndex = -1;
        }


        // =========================
        // SAVE
        // =========================
        private void btnSave_Click(object sender, EventArgs e)
        {
            string ad = txtAd.Text.Trim();
            string barkod = txtBarkod.Text.Trim();

            decimal fiyat = numFiyat.Value;
            int stok = (int)numStok.Value;
            int minStok = (int)numMinStok.Value;

            bool aktif = chkAktif.Checked;
            bool receteli = chkReceteli.Checked;

            int? kategoriId = cmbKategori.SelectedValue as int?;
            int? tedarikciId = cmbTedarikci.SelectedValue as int?;

            DateTime? sonKullanmaTarihi =
                dtpSonKullanma.Checked ? dtpSonKullanma.Value.Date : (DateTime?)null;

            // ===== VALIDATION =====
            if (string.IsNullOrWhiteSpace(ad))
            {
                MessageBox.Show("İlaç adı zorunludur.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (fiyat <= 0)
            {
                MessageBox.Show("Fiyat 0'dan büyük olmalıdır.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (kategoriId == null)
            {
                MessageBox.Show("Lütfen kategori seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // ===== BARKOD KONTROL =====
                if (!string.IsNullOrEmpty(barkod))
                {
                    object exists = DBHelper.ExecuteScalar(@"
                        SELECT 1
                        FROM ilac
                        WHERE barkod = @b
                    ",
                    "@b", barkod);

                    if (exists != null)
                    {
                        MessageBox.Show(
                            "Bu barkod ile kayıtlı bir ilaç zaten var.",
                            "Uyarı",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }
                }

                // ===== INSERT =====
                DBHelper.ExecuteNonQuery(@"
                    INSERT INTO ilac
                    (ad, barkod, kategori_id, tedarikci_id, fiyat, stok, min_stok,
                     receteli_mi, son_kullanma_tarihi, aktif)
                    VALUES
                    (@ad, @barkod, @kat, @ted, @fiyat, @stok, @minstok,
                     @receteli, @skt, @aktif)
                ",
                "@ad", ad,
                "@barkod", barkod,
                "@kat", kategoriId,
                "@ted", tedarikciId,
                "@fiyat", fiyat,
                "@stok", stok,
                "@minstok", minStok,
                "@receteli", receteli,
                "@skt", sonKullanmaTarihi,
                "@aktif", aktif);

                MessageBox.Show(
                    "İlaç başarıyla eklendi.",
                    "Bilgi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                GoToMedicineList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "İlaç eklenirken bir hata oluştu.\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================
        // CANCEL
        // =========================
        private void btnCancel_Click(object sender, EventArgs e)
        {
            GoToMedicineList();
        }

        // =========================
        // NAVIGATION
        // =========================
        private void GoToMedicineList()
        {
            var dashboard = this.FindForm() as AdminDashboard;
            if (dashboard == null) return;

            dashboard.LoadControl(new Admin_MedicineList());
        }
    }
}
