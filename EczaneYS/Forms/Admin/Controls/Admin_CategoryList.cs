using EczaneYS.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace EczaneYS.Forms.Admin.Controls
{
    public partial class Admin_CategoryList : UserControl
    {
        public Admin_CategoryList()
        {
            InitializeComponent();
            LoadCategories();
        }

        // =========================
        // KATEGORİLERİ YÜKLE
        // =========================
        private void LoadCategories()
        {
            try
            {
                DataTable dt = DBHelper.GetDataTable(@"
                    SELECT
                        kategori_id,
                        kategori_adi,
                        aktif
                    FROM kategori
                    ORDER BY kategori_adi
                ");

                dgv.DataSource = dt;

                dgv.Columns["kategori_id"].Visible = false;
                dgv.Columns["kategori_adi"].HeaderText = "Kategori Adı";
                dgv.Columns["aktif"].HeaderText = "Aktif";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Kategoriler yüklenirken hata oluştu.\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================
        // DÜZENLE (AKTİF / PASİF)
        // =========================
        private void btnEdit_Click(object sender, EventArgs e)
        {
            int? id = GetSelectedCategoryId();
            if (id == null) return;

            bool aktif = Convert.ToBoolean(dgv.SelectedRows[0].Cells["aktif"].Value);

            try
            {
                DBHelper.ExecuteNonQuery(@"
                    UPDATE kategori
                    SET aktif = @aktif
                    WHERE kategori_id = @id
                ",
                "@aktif", !aktif,
                "@id", id.Value);

                MessageBox.Show(
                    !aktif ? "Kategori aktif hale getirildi." : "Kategori pasif hale getirildi.",
                    "Bilgi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LoadCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Kategori güncellenirken hata oluştu.\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================
        // SİL (HARD DELETE)
        // =========================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int? id = GetSelectedCategoryId();
            if (id == null) return;

            if (MessageBox.Show(
                "Bu kategoriyi silmek istiyor musunuz?",
                "Kategori Sil",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                DBHelper.ExecuteNonQuery(@"
            DELETE FROM kategori
            WHERE kategori_id = @id
        ",
                "@id", id.Value);

                MessageBox.Show(
                    "Kategori silindi.",
                    "Bilgi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LoadCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Kategori silinirken hata oluştu.\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        // =========================
        // SEÇİLİ KATEGORİ ID
        // =========================
        private int? GetSelectedCategoryId()
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir kategori seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            return Convert.ToInt32(dgv.SelectedRows[0].Cells["kategori_id"].Value);
        }

        // =========================
        // EKLE
        // =========================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var dashboard = this.FindForm() as AdminDashboard;
            if (dashboard == null) return;

            dashboard.LoadControl(new Admin_AddCategory());
        }

    }
}
