using EczaneYS.Data;
using System;
using System.Windows.Forms;

namespace EczaneYS.Forms.Admin.Controls
{
    public partial class Admin_AddSupplier : UserControl
    {
        public Admin_AddSupplier()
        {
            InitializeComponent();
        }

        // ================= SAVE =================
        private void btnSave_Click(object sender, EventArgs e)
        {
            string firmaAdi = txtFirmaAdi.Text.Trim();
            string yetkili = txtYetkili.Text.Trim();
            string telefon = txtTelefon.Text.Trim();
            string email = txtEmail.Text.Trim();
            string adres = txtAdres.Text.Trim();
            bool aktif = chkAktif.Checked;

            if (string.IsNullOrWhiteSpace(firmaAdi))
            {
                MessageBox.Show("Firma adı boş olamaz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Email unique (boş değilse)
                if (!string.IsNullOrWhiteSpace(email))
                {
                    object emailExists = DBHelper.ExecuteScalar(
                        @"SELECT 1 FROM tedarikci 
                          WHERE LOWER(email)=LOWER(@e)",
                        "@e", email
                    );

                    if (emailExists != null)
                    {
                        MessageBox.Show("Bu e-posta zaten kayıtlı.", "Uyarı",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Firma adı unique
                object firmaExists = DBHelper.ExecuteScalar(
                    @"SELECT 1 FROM tedarikci 
                      WHERE LOWER(firma_adi)=LOWER(@f)",
                    "@f", firmaAdi
                );

                if (firmaExists != null)
                {
                    MessageBox.Show("Bu firma adı zaten mevcut.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // INSERT
                DBHelper.ExecuteNonQuery(@"
                    INSERT INTO tedarikci
                    (firma_adi, yetkili_kisi, telefon, adres, email, aktif)
                    VALUES
                    (@f, @y, @t, @a, @e, @ac)
                ",
                "@f", firmaAdi,
                "@y", string.IsNullOrWhiteSpace(yetkili) ? (object)DBNull.Value : yetkili,
                "@t", string.IsNullOrWhiteSpace(telefon) ? (object)DBNull.Value : telefon,
                "@a", string.IsNullOrWhiteSpace(adres) ? (object)DBNull.Value : adres,
                "@e", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email,
                "@ac", aktif);

                MessageBox.Show("Tedarikçi başarıyla eklendi.", "Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                GoToSupplierList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Tedarikçi eklenirken bir hata oluştu.\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ================= CANCEL =================
        private void btnCancel_Click(object sender, EventArgs e)
        {
            GoToSupplierList();
        }

        // ================= NAVIGATION =================
        private void GoToSupplierList()
        {
            var dashboard = this.FindForm() as AdminDashboard;
            if (dashboard == null) return;

            dashboard.LoadControl(new Admin_SupplierList());
        }

    }
}
