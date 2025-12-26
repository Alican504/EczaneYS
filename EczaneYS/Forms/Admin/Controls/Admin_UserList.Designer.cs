using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EczaneYS.Forms.Admin.Controls
{
    partial class Admin_UserList
    {
        private System.ComponentModel.IContainer components = null;

        private Guna2Panel mainPanel;
        private Guna2Panel topPanel;

        private Label lblTitle;
        private Guna2Button btnAdd;
        private Guna2Button btnEdit;
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();

            this.mainPanel = new Guna2Panel();
            this.dgvUsers = new Guna2DataGridView();
            this.topPanel = new Guna2Panel();
            this.lblTitle = new Label();
            this.btnAdd = new Guna2Button();
            this.btnEdit = new Guna2Button();
            this.btnDelete = new Guna2Button();

            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();

            this.dgvUsers.AllowUserToAddRows = false;

            // =========================
            // mainPanel
            // =========================
            this.mainPanel.BorderRadius = 18;
            this.mainPanel.Dock = DockStyle.Fill;
            this.mainPanel.FillColor = Color.White;
            this.mainPanel.Padding = new Padding(20);
            this.mainPanel.Controls.Add(this.dgvUsers);
            this.mainPanel.Controls.Add(this.topPanel);

            // =========================
            // topPanel
            // =========================
            this.topPanel.Dock = DockStyle.Top;
            this.topPanel.Height = 60;
            this.topPanel.FillColor = Color.Transparent;
            this.topPanel.Controls.Add(this.lblTitle);
            this.topPanel.Controls.Add(this.btnAdd);
            this.topPanel.Controls.Add(this.btnEdit);
            this.topPanel.Controls.Add(this.btnDelete);

            // =========================
            // lblTitle
            // =========================
            this.lblTitle.Dock = DockStyle.Left;
            this.lblTitle.Text = "Kullanıcı Listesi";
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(45, 45, 45);
            this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            this.lblTitle.Width = 260;

            // =========================
            // btnDelete
            // =========================
            this.btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnDelete.BorderRadius = 16;
            this.btnDelete.FillColor = Color.FromArgb(220, 53, 69);
            this.btnDelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnDelete.ForeColor = Color.White;
            this.btnDelete.Size = new Size(110, 38);
            this.btnDelete.Location = new Point(1930, 11);
            this.btnDelete.Text = "Sil";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            // =========================
            // btnEdit
            // =========================
            this.btnEdit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnEdit.BorderRadius = 16;
            this.btnEdit.FillColor = Color.FromArgb(255, 193, 7);
            this.btnEdit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnEdit.ForeColor = Color.Black;
            this.btnEdit.Size = new Size(130, 38);
            this.btnEdit.Location = new Point(1790, 11);
            this.btnEdit.Text = "Aktif / Pasif";
            this.btnEdit.Click += new EventHandler(this.btnEdit_Click);

            // =========================
            // btnAdd
            // =========================
            this.btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnAdd.BorderRadius = 16;
            this.btnAdd.FillColor = Color.FromArgb(0, 120, 215);
            this.btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.Size = new Size(150, 38);
            this.btnAdd.Location = new Point(1630, 11);
            this.btnAdd.Text = "Yeni Kullanıcı";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);

            // =========================
            // dgvUsers
            // =========================
            this.dgvUsers.Dock = DockStyle.Fill;
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.RowHeadersVisible = false;
            this.dgvUsers.GridColor = Color.LightGray;

            dataGridViewCellStyle1.BackColor = Color.White;
            this.dgvUsers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;

            dataGridViewCellStyle2.BackColor = Color.FromArgb(0, 120, 215);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.dgvUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUsers.ColumnHeadersHeight = 36;

            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9.5F);
            this.dgvUsers.DefaultCellStyle = dataGridViewCellStyle3;

            this.dgvUsers.CellContentClick += new DataGridViewCellEventHandler(this.dgvUsers_CellContentClick);

            // =========================
            // Admin_UserList
            // =========================
            this.BackColor = Color.FromArgb(245, 246, 250);
            this.Controls.Add(this.mainPanel);
            this.Name = "Admin_UserList";
            this.Size = new Size(2102, 1265);

            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.topPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
