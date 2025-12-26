using EczaneYS.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace EczaneYS.Forms.Admin.Controls
{
    public partial class Admin_EditUser : UserControl
    {
        private int _userId;

        public Admin_EditUser(int userId)
        {
            InitializeComponent();

            _userId = userId;
            LoadRoles();
            LoadUser();
        }

        // ================= ROLLER =================
        private void LoadRoles()
        {
            cmbRole.Items.Clear();
            cmbRole.Items.Add("ADMIN");
            cmbRole.Items.Add("PHARMACIST");
            cmbRole.Items.Add("PERSONNEL");
        }

        // ================= KULLANICI BİLGİLERİ =================
        private void LoadUser()
        {
            try
            {
                DataTable dt = DBHelper.GetDataTable(@"
                    SELECT username, full_name, email, phone, role, is_active
                    FROM users
                    WHERE id=@id",
                    "@id", _userId);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Kullanıcı bulunamadı.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    GoToUserList();
                    return;
                }

                DataRow r = dt.Rows[0];

                lblUsername.Text = "Kullanıcı: " + r["username"];
                txtFullName.Text = r["full_name"].ToString();
                txtEmail.Text = r["email"] == DBNull.Value ? "" : r["email"].ToString();
                txtPhone.Text = r["phone"] == DBNull.Value ? "" : r["phone"].ToString();

                string role = r["role"].ToString();
                cmbRole.SelectedItem = cmbRole.Items.Contains(role) ? role : "PERSONNEL";

                chkActive.Checked = Convert.ToBoolean(r["is_active"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Kullanıcı bilgileri yüklenirken hata oluştu.\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ================= SAVE =================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Ad Soyad boş olamaz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Lütfen rol seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int result = DBHelper.ExecuteNonQuery(@"
                    UPDATE users SET
                        full_name = @f,
                        email = @e,
                        phone = @p,
                        role = @r,
                        is_active = @a
                    WHERE id = @id",
                    "@f", txtFullName.Text.Trim(),
                    "@e", string.IsNullOrWhiteSpace(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text.Trim(),
                    "@p", string.IsNullOrWhiteSpace(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text.Trim(),
                    "@r", cmbRole.SelectedItem.ToString(),
                    "@a", chkActive.Checked,
                    "@id", _userId
                );

                if (result > 0)
                {
                    MessageBox.Show("Kullanıcı başarıyla güncellendi.", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GoToUserList();
                }
                else
                {
                    MessageBox.Show("Güncelleme başarısız.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Kullanıcı güncellenirken hata oluştu.\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ================= CANCEL =================
        private void btnCancel_Click(object sender, EventArgs e)
        {
            GoToUserList();
        }

        // ================= NAVIGATION =================
        private void GoToUserList()
        {
            if (this.Parent == null) return;

            this.Parent.Controls.Clear();
            this.Parent.Controls.Add(new Admin_UserList());
        }
    }
}
