using EczaneYS.Data;
using EczaneYS.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace EczaneYS.Forms.Admin.Controls
{
    public partial class Admin_UserList : UserControl
    {
        public Admin_UserList()
        {
            // 🔐 GÖRÜNTÜLEME YETKİSİ
            if (!PermissionService.HasPermission("USER_VIEW"))
            {
                MessageBox.Show("Kullanıcıları görüntüleme yetkiniz yok.");
                this.Enabled = false;
                return;
            }


            InitializeComponent();
            ApplyPermissions();
            LoadUsers();
        }
        

        // =========================
        // YETKİYE GÖRE BUTONLAR
        // =========================
        private void ApplyPermissions()
        {
            btnAdd.Visible = PermissionService.HasPermission("USER_ADD");
            btnEdit.Visible = PermissionService.HasPermission("USER_UPDATE");
            btnDelete.Visible = PermissionService.HasPermission("USER_DELETE");
        }

        // =========================
        // KULLANICILARI YÜKLE
        // =========================
        private void LoadUsers()
        {
            DataTable dt = DBHelper.GetDataTable(@"
                SELECT
                    k.kullanici_id,
                    k.kullanici_adi,
                    k.ad,
                    k.soyad,
                    r.rol_adi,
                    k.aktif
                FROM kullanici k
                JOIN rol r ON r.rol_id = k.rol_id
                ORDER BY k.kullanici_adi
            ");

            dgvUsers.DataSource = dt;

            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.ReadOnly = true;
            dgvUsers.EditMode = DataGridViewEditMode.EditProgrammatically;


            dgvUsers.Columns["kullanici_id"].Visible = false;
            dgvUsers.Columns["kullanici_adi"].HeaderText = "Kullanıcı Adı";
            dgvUsers.Columns["ad"].HeaderText = "Ad";
            dgvUsers.Columns["soyad"].HeaderText = "Soyad";
            dgvUsers.Columns["rol_adi"].HeaderText = "Rol";
            dgvUsers.Columns["aktif"].HeaderText = "Aktif";

            dgvUsers.ClearSelection();
            dgvUsers.CurrentCell = null;

            dgvUsers.Columns["aktif"].ReadOnly = true;

        }

        // =========================
        // EKLE
        // =========================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Parent = panelContent
            var parentPanel = this.Parent;

            parentPanel.Controls.Clear();

            var addUserControl = new Admin_UserControl();
            addUserControl.Dock = DockStyle.Fill;

            parentPanel.Controls.Add(addUserControl);
        }


        // =========================
        // AKTİF / PASİF
        // =========================
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
                return;

            if (dgvUsers.SelectedRows[0].Cells["kullanici_id"].Value == DBNull.Value)
            {
                MessageBox.Show("Geçersiz satır.", "Uyarı");
                return;
            }

            int userId = Convert.ToInt32(
                dgvUsers.SelectedRows[0].Cells["kullanici_id"].Value
            );

            bool aktif = Convert.ToBoolean(
                dgvUsers.SelectedRows[0].Cells["aktif"].Value
            );

            DBHelper.ExecuteNonQuery(@"
                UPDATE kullanici
                SET aktif = @aktif
                WHERE kullanici_id = @id",
                "@aktif", !aktif,
                "@id", userId
            );

            LoadUsers();
        }

        // =========================
        // SİL (HARD DELETE)
        // =========================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
                return;

            if (dgvUsers.SelectedRows[0].Cells["kullanici_id"].Value == DBNull.Value)
            {
                MessageBox.Show("Geçersiz satır.", "Uyarı");
                return;
            }

            int userId = Convert.ToInt32(
                dgvUsers.SelectedRows[0].Cells["kullanici_id"].Value
            );

            if (MessageBox.Show(
                "Bu kullanıcıyı silmek istiyor musunuz?",
                "Sil",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            DBHelper.ExecuteNonQuery(@"
        DELETE FROM kullanici
        WHERE kullanici_id = @id
    ",
            "@id", userId
            );

            LoadUsers();
        }


        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.RowIndex == dgvUsers.Rows.Count - 1) return;
        }

        private void dgvUsers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvUsers.AllowUserToAddRows = false;

            // tamamen boş satır varsa kaldır
            for (int i = dgvUsers.Rows.Count - 1; i >= 0; i--)
            {
                var r = dgvUsers.Rows[i];
                if (r.IsNewRow) continue;

                bool empty = true;
                foreach (DataGridViewCell c in r.Cells)
                {
                    if (c.Value != null && c.Value != DBNull.Value && c.Value.ToString().Trim() != "")
                    {
                        empty = false;
                        break;
                    }
                }

                if (empty)
                    dgvUsers.Rows.RemoveAt(i);
            }
        }


    }
}
