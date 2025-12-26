using EczaneYS.Data;
using System;
using System.Windows.Forms;

namespace EczaneYS.Forms.Admin.Controls
{
    public partial class Admin_AddCategory : UserControl
    {
        public Admin_AddCategory()
        {
            InitializeComponent();
        }

        // ================= SAVE =================
        private void btnSave_Click(object sender, EventArgs e)
        {
            string kategoriAdi = txtName.Text.Trim();
            bool aktif = chkAktif.Checked;

            if (string.IsNullOrWhiteSpace(kategoriAdi))
            {
                MessageBox.Show("Kategori adı boş olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Aynı isimde kategori var mı?
                object exists = DBHelper.ExecuteScalar(@"
                    SELECT 1
                    FROM kategori
                    WHERE LOWER(kategori_adi) = LOWER(@adi)
                ",
                "@adi", kategoriAdi);

                if (exists != null)
                {
                    MessageBox.Show("Bu kategori zaten mevcut.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // INSERT
                DBHelper.ExecuteNonQuery(@"
                    INSERT INTO kategori (kategori_adi, aktif)
                    VALUES (@adi, @aktif)
                ",
                "@adi", kategoriAdi,
                "@aktif", aktif);

                MessageBox.Show("Kategori başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GoToCategoryList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Kategori eklenirken bir hata oluştu.\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ================= CANCEL =================
        private void btnCancel_Click(object sender, EventArgs e)
        {
            GoToCategoryList();
        }

        // ================= NAVIGATION =================
        private void GoToCategoryList()
        {
            var dashboard = this.FindForm() as AdminDashboard;
            if (dashboard == null) return;

            dashboard.LoadControl(new Admin_CategoryList());
        }

    }
}
