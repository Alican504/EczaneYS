using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace EczaneYS.Forms.Admin.Controls
{
    partial class Admin_CategoryList
    {
        private System.ComponentModel.IContainer components = null;

        private Guna2Panel mainPanel;
        private Guna2Panel topPanel;

        private Label lblTitle;
        private Guna2Button btnAdd;
        private Guna2Button btnEdit;
        private Guna2Button btnDelete;

        private Guna2DataGridView dgv;

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
            this.dgv = new Guna2DataGridView();
            this.topPanel = new Guna2Panel();

            this.lblTitle = new Label();
            this.btnAdd = new Guna2Button();      // ✅ EKSİK OLAN SATIR
            this.btnEdit = new Guna2Button();
            this.btnDelete = new Guna2Button();

            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();

            // ================= MAIN PANEL =================
            this.mainPanel.BorderRadius = 18;
            this.mainPanel.Dock = DockStyle.Fill;
            this.mainPanel.FillColor = Color.White;
            this.mainPanel.Padding = new Padding(20);
            this.mainPanel.Controls.Add(this.dgv);
            this.mainPanel.Controls.Add(this.topPanel);

            // ================= TOP PANEL =================
            this.topPanel.Dock = DockStyle.Top;
            this.topPanel.Size = new Size(2004, 80);
            this.topPanel.FillColor = Color.Transparent;

            this.topPanel.Controls.Add(this.lblTitle);
            this.topPanel.Controls.Add(this.btnAdd);
            this.topPanel.Controls.Add(this.btnEdit);
            this.topPanel.Controls.Add(this.btnDelete);

            // ================= TITLE =================
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(45, 45, 45);
            this.lblTitle.Location = new Point(10, 20);
            this.lblTitle.Text = "Kategoriler";

            // ================= BTN ADD =================
            this.btnAdd.BorderRadius = 14;
            this.btnAdd.FillColor = Color.FromArgb(0, 120, 215);
            this.btnAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.Location = new Point(360, 20);
            this.btnAdd.Size = new Size(110, 38);
            this.btnAdd.Text = "Ekle";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // ================= BTN EDIT =================
            this.btnEdit.BorderRadius = 14;
            this.btnEdit.FillColor = Color.FromArgb(255, 140, 0);
            this.btnEdit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnEdit.ForeColor = Color.White;
            this.btnEdit.Location = new Point(520, 20);
            this.btnEdit.Size = new Size(140, 40);
            this.btnEdit.Text = "Aktif / Pasif";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            // ================= BTN DELETE =================
            this.btnDelete.BorderRadius = 14;
            this.btnDelete.FillColor = Color.FromArgb(220, 53, 69);
            this.btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnDelete.ForeColor = Color.White;
            this.btnDelete.Location = new Point(680, 20);
            this.btnDelete.Size = new Size(140, 40);
            this.btnDelete.Text = "Sil";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // ================= DATAGRID =================
            this.dgv.Dock = DockStyle.Fill;
            this.dgv.Location = new Point(20, 100);
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;

            // ================= FORM =================
            this.Controls.Add(this.mainPanel);
            this.Size = new Size(2044, 998);
            this.BackColor = Color.FromArgb(245, 246, 250);

            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
