using EczaneYS.Data;
using EczaneYS.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace EczaneYS.Forms.Pharmacist
{
    public partial class Pharmacist_MedicineAdd : Form
    {
        public Pharmacist_MedicineAdd()
        {
            InitializeComponent();
        }

        private void Pharmacist_MedicineAdd_Load(object sender, EventArgs e)
        {
            if (!PermissionService.HasPermission("MEDICINE_ADD"))
            {
                MessageBox.Show("İlaç ekleme yetkiniz yok.");
                this.Close();
                return;
            }

            LoadCategories();
        }

        // ===============================
        // KATEGORİ LİSTESİ
        // ===============================
        private void LoadCategories()
        {
            DataTable dt = DBHelper.GetDataTable(@"
                SELECT kategori_id, kategori_adi
                FROM kategori
                WHERE aktif = TRUE
                ORDER BY kategori_adi
            ");

            cmbCategory.DataSource = dt;
            cmbCategory.DisplayMember = "kategori_adi";
            cmbCategory.ValueMember = "kategori_id";
            cmbCategory.SelectedIndex = -1;
        }

        // ===============================
        // KAYDET
        // ===============================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtBarcode.Text) ||
                cmbCategory.SelectedValue == null ||
                !decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Lütfen tüm alanları doğru doldurunuz.");
                return;
            }

            try
            {
                DBHelper.ExecuteNonQuery(@"
                    INSERT INTO ilac
                        (ad, barkod, fiyat, stok, receteli_mi, kategori_id, aktif)
                    VALUES
                        (@ad, @barkod, @fiyat, @stok, @receteli, @kategori, TRUE)",
                    "@ad", txtName.Text.Trim(),
                    "@barkod", txtBarcode.Text.Trim(),
                    "@fiyat", price,
                    "@stok", (int)numStock.Value,
                    "@receteli", chkPrescription.Checked,
                    "@kategori", Convert.ToInt32(cmbCategory.SelectedValue)
                );

                MessageBox.Show("İlaç başarıyla eklendi ✅");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "İlaç eklenirken hata oluştu:\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
