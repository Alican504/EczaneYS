using EczaneYS.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace EczaneYS.Forms.Admin.Controls
{
    public partial class Admin_MedicineList : UserControl
    {
        public Admin_MedicineList()
        {
            InitializeComponent();
            LoadMedicines();
        }

        // =========================
        // İLAÇLARI YÜKLE
        // =========================
        private void LoadMedicines()
        {
            try
            {
                DataTable dt = DBHelper.GetDataTable(@"
                    SELECT
                        ilac_id,
                        ad AS ilac_adi,
                        stok,
                        min_stok,
                        aktif
                    FROM ilac
                    ORDER BY ad
                ");

                dgv.DataSource = dt;

                dgv.Columns["ilac_id"].Visible = false;
                dgv.Columns["ilac_adi"].HeaderText = "İlaç Adı";
                dgv.Columns["stok"].HeaderText = "Stok";
                dgv.Columns["min_stok"].HeaderText = "Min. Stok";
                dgv.Columns["aktif"].HeaderText = "Aktif";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "İlaçlar yüklenirken hata oluştu.\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================
        // EKLE
        // =========================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            GoToAddMedicine();
        }

        // =========================
        // AKTİF / PASİF
        // =========================
        private void btnEdit_Click(object sender, EventArgs e)
        {
            int? id = GetSelectedMedicineId();
            if (id == null) return;

            bool aktif = Convert.ToBoolean(dgv.SelectedRows[0].Cells["aktif"].Value);

            try
            {
                DBHelper.ExecuteNonQuery(@"
                    UPDATE ilac
                    SET aktif = @aktif
                    WHERE ilac_id = @id
                ",
                "@aktif", !aktif,
                "@id", id.Value);

                MessageBox.Show(
                    !aktif ? "İlaç aktif hale getirildi." : "İlaç pasif hale getirildi.",
                    "Bilgi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LoadMedicines();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "İlaç güncellenirken hata oluştu.\n\n" + ex.Message,
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
            int? id = GetSelectedMedicineId();
            if (id == null) return;

            if (MessageBox.Show(
                "Bu ilacı silmek istiyor musunuz?",
                "İlaç Sil",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                DBHelper.ExecuteNonQuery(@"
            DELETE FROM ilac
            WHERE ilac_id = @id
        ",
                "@id", id.Value);

                MessageBox.Show(
                    "İlaç silindi.",
                    "Bilgi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LoadMedicines();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "İlaç silinirken hata oluştu.\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        // =========================
        // SEÇİLİ İLAÇ ID
        // =========================
        private int? GetSelectedMedicineId()
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Lütfen bir ilaç seçin.",
                    "Uyarı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return null;
            }

            return Convert.ToInt32(dgv.SelectedRows[0].Cells["ilac_id"].Value);
        }

        // =========================
        // NAVIGATION
        // =========================
        private void GoToAddMedicine()
        {
            var dashboard = this.FindForm() as AdminDashboard;
            if (dashboard == null)
            {
                MessageBox.Show("Dashboard bulunamadı.");
                return;
            }

            dashboard.LoadControl(new Admin_AddMedicine());
        }

    }
}
