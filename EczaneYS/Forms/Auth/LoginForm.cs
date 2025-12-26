using EczaneYS.Forms.Admin;
using EczaneYS.Forms.Personnel;
using EczaneYS.Forms.Pharmacist;
using EczaneYS.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace EczaneYS.Forms.Auth
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş olamaz!");
                return;
            }

            try
            {
                string query = @"
                    SELECT k.kullanici_id, k.sifre, r.rol_kodu
FROM kullanici k
JOIN rol r ON r.rol_id = k.rol_id
WHERE k.kullanici_adi = @u
  AND k.aktif = TRUE
LIMIT 1;

                ";

                DataTable dt = DBHelper.GetDataTable(
                    query,
                    "@u", username,
                    "@p", password
                );

                if (dt.Rows.Count != 1)
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
                    return;
                }

                DataRow row = dt.Rows[0];

                string dbHash = row["sifre"].ToString();
                string inputHash = ComputeSha256Hash(password);

                if (dbHash != inputHash)
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
                    return;
                }


                int userId = Convert.ToInt32(row["kullanici_id"]);
                string role = row["rol_kodu"].ToString();

                // 🔐 AUTH CONTEXT
                AuthContext.UserId = userId;
                AuthContext.Role = role;
                AuthContext.Username = username;

                // 🔀 ROL YÖNLENDİRME
                if (role == "ADMIN")
                {
                    new AdminDashboard(userId, role).Show();
                }
                else if (role == "PHARMACIST")
                {
                    new PharmacistForm(userId, role).Show();
                }
                else if (role == "PERSONNEL")
                {
                    new PersonnelForm(userId, role).Show();
                }
                else
                {
                    MessageBox.Show("Tanımsız rol!");
                    return;
                }

                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giriş hatası:\n" + ex.Message);
            }
        }

        private void panelLeft_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelRight_Paint(object sender, PaintEventArgs e)
        {

        }


        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panelRight_Paint_1(object sender, PaintEventArgs e)
        {

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
