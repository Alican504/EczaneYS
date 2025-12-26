using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Drawing;

namespace EczaneYS.Forms.Admin.Controls
{
    partial class Admin_CustomerList
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView dgvCustomers;
        private Guna2Button btnAdd;
        private Guna2Button btnEdit;
        private Guna2Button btnDelete;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvCustomers = new DataGridView();
            this.btnAdd = new Guna2Button();
            this.btnEdit = new Guna2Button();
            this.btnDelete = new Guna2Button();

            // dgvCustomers
            this.dgvCustomers.Dock = DockStyle.Top;
            this.dgvCustomers.Height = 380;
            this.dgvCustomers.ReadOnly = true;
            this.dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomers.MultiSelect = false;
            this.dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // btnAdd
            this.btnAdd.Text = "Ekle";
            this.btnAdd.Location = new Point(20, 400);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnEdit
            this.btnEdit.Text = "Güncelle";
            this.btnEdit.Location = new Point(220, 400);
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            // btnDelete
            this.btnDelete.Text = "Sil";
            this.btnDelete.Location = new Point(420, 400);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // UserControl
            this.Controls.Add(this.dgvCustomers);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Size = new Size(900, 480);
        }
    }
}
