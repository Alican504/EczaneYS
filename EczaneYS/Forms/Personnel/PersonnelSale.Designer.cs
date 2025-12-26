using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EczaneYS.Forms.Personnel
{
    partial class PersonnelSale
    {
        private System.ComponentModel.IContainer components = null;

        private Guna2Panel panelHeader;
        private Guna2HtmlLabel lblTitle;

        private Guna2Panel panelSaleCreate;
        private Guna2ComboBox cmbMedicine;
        private Guna2NumericUpDown numQty;
        private Guna2Button btnAddToCart;
        private Guna2DataGridView dgvCart;
        private Label lblTotal;
        private Guna2Button btnCompleteSale;

        private Guna2DataGridView dgvSales;
        private Guna2Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelHeader = new Guna2Panel();
            this.lblTitle = new Guna2HtmlLabel();
            this.panelSaleCreate = new Guna2Panel();
            this.cmbMedicine = new Guna2ComboBox();
            this.numQty = new Guna2NumericUpDown();
            this.btnAddToCart = new Guna2Button();
            this.dgvCart = new Guna2DataGridView();
            this.lblTotal = new Label();
            this.btnCompleteSale = new Guna2Button();
            this.dgvSales = new Guna2DataGridView();
            this.btnClose = new Guna2Button();

            this.panelHeader.SuspendLayout();
            this.panelSaleCreate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).BeginInit();
            this.SuspendLayout();

            // ================= HEADER =================
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.FillColor = Color.FromArgb(0, 123, 255);
            this.panelHeader.Size = new Size(1050, 60);
            this.panelHeader.Controls.Add(this.lblTitle);

            this.lblTitle.BackColor = Color.Transparent;
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location = new Point(20, 15);
            this.lblTitle.Text = "Personel Satış";

            // ================= SALE CREATE PANEL =================
            this.panelSaleCreate.BorderRadius = 12;
            this.panelSaleCreate.BorderThickness = 1;
            this.panelSaleCreate.BorderColor = Color.LightGray;
            this.panelSaleCreate.FillColor = Color.White;
            this.panelSaleCreate.Location = new Point(20, 80);
            this.panelSaleCreate.Size = new Size(480, 520);

            this.cmbMedicine.Location = new Point(20, 20);
            this.cmbMedicine.Size = new Size(300, 36);
            this.cmbMedicine.DropDownStyle = ComboBoxStyle.DropDownList;

            this.numQty.Location = new Point(330, 20);
            this.numQty.Minimum = 1;
            this.numQty.Value = 1;
            this.numQty.Size = new Size(120, 36);

            this.btnAddToCart.Text = "Sepete Ekle";
            this.btnAddToCart.Location = new Point(20, 70);
            this.btnAddToCart.Size = new Size(430, 40);
            this.btnAddToCart.Click += new EventHandler(this.btnAddToCart_Click);

            this.dgvCart.Location = new Point(20, 130);
            this.dgvCart.Size = new Size(430, 260);
            this.dgvCart.ReadOnly = true;
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AllowUserToDeleteRows = false;
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.lblTotal.Location = new Point(20, 400);
            this.lblTotal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblTotal.Text = "Toplam: 0 ₺";

            this.btnCompleteSale.Text = "Satışı Tamamla";
            this.btnCompleteSale.Location = new Point(20, 440);
            this.btnCompleteSale.Size = new Size(430, 45);
            this.btnCompleteSale.Click += new EventHandler(this.btnCompleteSale_Click);

            this.panelSaleCreate.Controls.Add(this.cmbMedicine);
            this.panelSaleCreate.Controls.Add(this.numQty);
            this.panelSaleCreate.Controls.Add(this.btnAddToCart);
            this.panelSaleCreate.Controls.Add(this.dgvCart);
            this.panelSaleCreate.Controls.Add(this.lblTotal);
            this.panelSaleCreate.Controls.Add(this.btnCompleteSale);

            // ================= SALES LIST =================
            this.dgvSales.Location = new Point(520, 80);
            this.dgvSales.Size = new Size(500, 520);
            this.dgvSales.ReadOnly = true;
            this.dgvSales.AllowUserToAddRows = false;
            this.dgvSales.AllowUserToDeleteRows = false;
            this.dgvSales.RowHeadersVisible = false;
            this.dgvSales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // ================= CLOSE =================
            this.btnClose.Text = "Kapat";
            this.btnClose.Location = new Point(900, 620);
            this.btnClose.Size = new Size(120, 40);
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            // ================= FORM =================
            this.ClientSize = new Size(1050, 680);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelSaleCreate);
            this.Controls.Add(this.dgvSales);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Personel Satış";
            this.Load += new EventHandler(this.PersonnelSale_Load);

            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelSaleCreate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
