using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EczaneYS.Forms.Admin
{
    partial class Admin_UserControl
    {
        private System.ComponentModel.IContainer components = null;

        private Guna2Panel mainPanel;
        private Guna2Panel formPanel;

        private Label lblTitle;

        private Guna2TextBox txtUsername;
        private Guna2TextBox txtPassword;
        private Guna2TextBox txtFirstName;
        private Guna2TextBox txtLastName;

        private Guna2ComboBox cmbRole;
        private Guna2CheckBox chkActive;

        private Guna2Button btnAdd;
        private Guna2Button btnUpdate;
        private Guna2Button btnDelete;

        private Guna2DataGridView dgvUsers;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            var headerStyle = new DataGridViewCellStyle();
            var rowStyle = new DataGridViewCellStyle();

            this.mainPanel = new Guna2Panel();
            this.formPanel = new Guna2Panel();
            this.dgvUsers = new Guna2DataGridView();

            this.lblTitle = new Label();

            this.txtUsername = new Guna2TextBox();
            this.txtPassword = new Guna2TextBox();
            this.txtFirstName = new Guna2TextBox();
            this.txtLastName = new Guna2TextBox();

            this.cmbRole = new Guna2ComboBox();
            this.chkActive = new Guna2CheckBox();

            this.btnAdd = new Guna2Button();
            this.btnUpdate = new Guna2Button();
            this.btnDelete = new Guna2Button();

            this.SuspendLayout();

            // ================= MAIN PANEL =================
            this.mainPanel.Dock = DockStyle.Fill;
            this.mainPanel.Padding = new Padding(20);
            this.mainPanel.FillColor = Color.White;
            this.mainPanel.BorderRadius = 18;

            // ================= FORM PANEL =================
            this.formPanel.Dock = DockStyle.Top;
            this.formPanel.Height = 420;
            this.formPanel.FillColor = Color.Transparent;

            // Title
            this.lblTitle.Text = "Kullanıcı Yönetimi";
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.Location = new Point(10, 10);
            this.lblTitle.Size = new Size(300, 35);

            int x = 20;
            int y = 60;
            int w = 320;
            int h = 36;
            int gap = 12;

            // Username
            this.txtUsername.PlaceholderText = "Kullanıcı Adı";
            this.txtUsername.SetBounds(x, y, w, h);

            // Password
            y += h + gap;
            this.txtPassword.PlaceholderText = "Şifre";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.SetBounds(x, y, w, h);

            // First Name
            y += h + gap;
            this.txtFirstName.PlaceholderText = "Ad";
            this.txtFirstName.SetBounds(x, y, w, h);

            // Last Name
            y += h + gap;
            this.txtLastName.PlaceholderText = "Soyad";
            this.txtLastName.SetBounds(x, y, w, h);

            // Role
            y += h + gap;
            this.cmbRole.SetBounds(x, y, w, h);
            this.cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRole.BorderRadius = 8;

            // Active
            y += h + gap;
            this.chkActive.Text = "Aktif";
            this.chkActive.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.chkActive.Location = new Point(x, y);

            // Buttons
            y += 45;
            this.btnAdd.Text = "Ekle";
            this.btnAdd.FillColor = Color.FromArgb(0, 120, 215);
            this.btnAdd.SetBounds(x, y, 100, 38);
            this.btnAdd.Click += btnAdd_Click;

            this.btnUpdate.Text = "Güncelle";
            this.btnUpdate.FillColor = Color.FromArgb(255, 193, 7);
            this.btnUpdate.ForeColor = Color.Black;
            this.btnUpdate.SetBounds(x + 110, y, 110, 38);
            this.btnUpdate.Click += btnUpdate_Click;

            this.btnDelete.Text = "Sil";
            this.btnDelete.FillColor = Color.FromArgb(220, 53, 69);
            this.btnDelete.SetBounds(x + 230, y, 90, 38);
            this.btnDelete.Click += btnDelete_Click;

            // Add controls to formPanel
            this.formPanel.Controls.AddRange(new Control[]
            {
                lblTitle,
                txtUsername, txtPassword,
                txtFirstName, txtLastName,
                cmbRole, chkActive,
                btnAdd, btnUpdate, btnDelete
            });

            // ================= GRID =================
            headerStyle.BackColor = Color.FromArgb(0, 120, 215);
            headerStyle.ForeColor = Color.White;
            headerStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            rowStyle.Font = new Font("Segoe UI", 9.5F);

            this.dgvUsers.Dock = DockStyle.Fill;
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersVisible = false;
            this.dgvUsers.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvUsers.DefaultCellStyle = rowStyle;
            this.dgvUsers.CellClick += dgvUsers_CellClick;

            // ================= FINAL =================
            this.mainPanel.Controls.Add(this.dgvUsers);
            this.mainPanel.Controls.Add(this.formPanel);

            this.Controls.Add(this.mainPanel);
            this.BackColor = Color.FromArgb(245, 246, 250);
            this.Size = new Size(1055, 620);
            this.Load += Admin_UserControl_Load;

            this.ResumeLayout(false);
        }
    }
}
