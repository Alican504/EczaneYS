using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace EczaneYS.Forms.Admin.Controls
{
    partial class Admin_SupplierList
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
            this.mainPanel = new Guna2Panel();
            this.topPanel = new Guna2Panel();
            this.lblTitle = new Label();
            this.btnAdd = new Guna2Button();
            this.btnEdit = new Guna2Button();
            this.btnDelete = new Guna2Button();
            this.dgv = new Guna2DataGridView();

            this.mainPanel.SuspendLayout();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();

            // ================= MAIN PANEL =================
            this.mainPanel.Dock = DockStyle.Fill;
            this.mainPanel.Padding = new Padding(20);
            this.mainPanel.FillColor = Color.White;
            this.mainPanel.BorderRadius = 18;
            this.mainPanel.Controls.Add(this.dgv);
            this.mainPanel.Controls.Add(this.topPanel);

            // ================= TOP PANEL =================
            this.topPanel.Dock = DockStyle.Top;
            this.topPanel.Height = 80;
            this.topPanel.FillColor = Color.Transparent;

            // ================= TITLE =================
            this.lblTitle.Text = "Tedarikçiler";
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.Location = new Point(10, 20);
            this.lblTitle.Size = new Size(300, 40);

            // ================= BTN ADD =================
            this.btnAdd.Text = "Ekle";
            this.btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnAdd.FillColor = Color.FromArgb(40, 167, 69);
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.BorderRadius = 14;
            this.btnAdd.Size = new Size(100, 36);
            this.btnAdd.Location = new Point(360, 22);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // ================= BTN EDIT =================
            this.btnEdit.Text = "Aktif / Pasif";
            this.btnEdit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnEdit.FillColor = Color.FromArgb(255, 193, 7);
            this.btnEdit.ForeColor = Color.Black;
            this.btnEdit.BorderRadius = 14;
            this.btnEdit.Size = new Size(130, 36);
            this.btnEdit.Location = new Point(480, 22);
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            // ================= BTN DELETE =================
            this.btnDelete.Text = "Sil";
            this.btnDelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnDelete.FillColor = Color.FromArgb(220, 53, 69);
            this.btnDelete.ForeColor = Color.White;
            this.btnDelete.BorderRadius = 14;
            this.btnDelete.Size = new Size(100, 36);
            this.btnDelete.Location = new Point(640, 22);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // ================= DATAGRID =================
            this.dgv.Dock = DockStyle.Fill;
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;

            // ================= ADD CONTROLS =================
            this.topPanel.Controls.Add(this.lblTitle);
            this.topPanel.Controls.Add(this.btnAdd);
            this.topPanel.Controls.Add(this.btnEdit);
            this.topPanel.Controls.Add(this.btnDelete);

            this.Controls.Add(this.mainPanel);
            this.Size = new Size(2044, 998);

            this.mainPanel.ResumeLayout(false);
            this.topPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
