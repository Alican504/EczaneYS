using EczaneYS.Data;
using EczaneYS.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace EczaneYS.Forms.Admin
{
    public partial class Admin_UserControl : UserControl
    {
        public Admin_UserControl()
        {
            InitializeComponent();

            // 🔐 SADECE ADMIN
            if (!PermissionService.HasPermission("USER_VIEW"))
            {
                MessageBox.Show("Kullanıcı yönetimine erişim yetkiniz yok.");
                this.Enabled = false;
                return;
            }
        }

        private void Admin_UserControl_Load(object sender, EventArgs e)
        {
            LoadRoles();
            LoadUsers();
            ApplyPermissions();
        }

        // =========================
        // YETKİLER
        // =========================
        private void ApplyPermissions()
        {
            btnAdd.Visible = PermissionService.HasPermission("USER_ADD");
            btnUpdate.Visible = PermissionService.HasPermission("USER_UPDATE");
            btnDelete.Visible = PermissionService.HasPermission("USER_DELETE");
        }

        // =========================
        // ROLLER
        // =========================
        private void LoadRoles()
        {
            DataTable dt = DBHelper.GetDataTable(
                "SELECT rol_id, rol_adi FROM rol WHERE aktif = TRUE");

            cmbRole.DisplayMember = "rol_adi";
            cmbRole.ValueMember = "rol_id";
            cmbRole.DataSource = dt;
        }

        // =========================
        // KULLANICILAR
        // =========================
        private void LoadUsers()
        {
            DataTable dt = DBHelper.GetDataTable(@"
                SELECT 
                    k.kullanici_id,
                    k.kullanici_adi,
                    k.ad,
                    k.soyad,
                    r.rol_id,
                    r.rol_adi,
                    k.aktif
                FROM kullanici k
                JOIN rol r ON k.rol_id = r.rol_id
            ");

            dgvUsers.DataSource = dt;
            dgvUsers.AllowUserToAddRows = false;

            dgvUsers.Columns["kullanici_id"].Visible = false;
            dgvUsers.Columns["rol_id"].Visible = false;

            dgvUsers.Columns["kullanici_adi"].HeaderText = "Kullanıcı Adı";
            dgvUsers.Columns["ad"].HeaderText = "Ad";
            dgvUsers.Columns["soyad"].HeaderText = "Soyad";
            dgvUsers.Columns["rol_adi"].HeaderText = "Rol";
            dgvUsers.Columns["aktif"].HeaderText = "Aktif";

            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.MultiSelect = false;
        }

        // =========================
        // EKLE
        // =========================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = DBHelper.GetDataTable(
    "SELECT 1 FROM kullanici WHERE kullanici_adi = @u",
    "@u", txtUsername.Text.Trim()
);

            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Bu kullanıcı adı zaten kullanılıyor.");
                return;
            }


            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Şifre boş olamaz");
                return;
            }

            string hashedPassword = ComputeSha256Hash(txtPassword.Text.Trim());

            DBHelper.ExecuteNonQuery(@"
                INSERT INTO kullanici
                (kullanici_adi, sifre, rol_id, ad, soyad, aktif)
                VALUES (@u,@p,@r,@a,@s,@aktif)",
                "@u", txtUsername.Text.Trim(),
                "@p", hashedPassword,
                "@r", cmbRole.SelectedValue,
                "@a", txtFirstName.Text.Trim(),
                "@s", txtLastName.Text.Trim(),
                "@aktif", chkActive.Checked
            );

            MessageBox.Show("Kullanıcı eklendi");
            ClearForm();
            LoadUsers();
        }

        // =========================
        // GÜNCELLE
        // =========================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvUsers.CurrentRow.Cells["kullanici_id"].Value);

            string passwordSql = "";
            object[] parameters;

            if (!string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                passwordSql = ", sifre=@p";
                parameters = new object[]
                {
                    "@u", txtUsername.Text.Trim(),
                    "@p", ComputeSha256Hash(txtPassword.Text.Trim()),
                    "@r", cmbRole.SelectedValue,
                    "@a", txtFirstName.Text.Trim(),
                    "@s", txtLastName.Text.Trim(),
                    "@aktif", chkActive.Checked,
                    "@id", id
                };
            }
            else
            {
                parameters = new object[]
                {
                    "@u", txtUsername.Text.Trim(),
                    "@r", cmbRole.SelectedValue,
                    "@a", txtFirstName.Text.Trim(),
                    "@s", txtLastName.Text.Trim(),
                    "@aktif", chkActive.Checked,
                    "@id", id
                };
            }

            DBHelper.ExecuteNonQuery($@"
                UPDATE kullanici SET
                    kullanici_adi=@u,
                    rol_id=@r,
                    ad=@a,
                    soyad=@s,
                    aktif=@aktif
                    {passwordSql}
                WHERE kullanici_id=@id",
                parameters
            );

            MessageBox.Show("Kullanıcı güncellendi");
            ClearForm();
            LoadUsers();
        }

        // =========================
        // SİL (HARD DELETE)
        // =========================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null) return;

            if (MessageBox.Show(
                "Bu kullanıcı silinsin mi?",
                "Onay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            int id = Convert.ToInt32(
                dgvUsers.CurrentRow.Cells["kullanici_id"].Value
            );

            DBHelper.ExecuteNonQuery(@"
        DELETE FROM kullanici
        WHERE kullanici_id=@id
    ",
            "@id", id
            );

            MessageBox.Show("Kullanıcı silindi");
            ClearForm();
            LoadUsers();
        }


        // =========================
        // GRID SEÇİM
        // =========================
        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUsers.CurrentRow == null) return;

            txtUsername.Text = dgvUsers.CurrentRow.Cells["kullanici_adi"].Value.ToString();
            txtPassword.Text = "";
            txtFirstName.Text = dgvUsers.CurrentRow.Cells["ad"].Value.ToString();
            txtLastName.Text = dgvUsers.CurrentRow.Cells["soyad"].Value.ToString();
            chkActive.Checked = Convert.ToBoolean(dgvUsers.CurrentRow.Cells["aktif"].Value);

            cmbRole.SelectedValue =
                dgvUsers.CurrentRow.Cells["rol_id"].Value;
        }

        // =========================
        // YARDIMCILAR
        // =========================
        private void ClearForm()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            chkActive.Checked = true;
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(
                    System.Text.Encoding.UTF8.GetBytes(rawData));

                return BitConverter
                    .ToString(bytes)
                    .Replace("-", "")
                    .ToLower();
            }
        }
    }
}
