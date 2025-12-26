using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace EczaneYS.Forms.Pharmacist
{
    partial class PharmacistSale
    {
        private System.ComponentModel.IContainer components = null;

        private Guna2Panel panelHeader;
        private Guna2HtmlLabel lblTitle;

        private Guna2HtmlLabel lblCategory;
        private Guna2ComboBox cmbCategory;

        private Guna2HtmlLabel lblMedicine;
        private Guna2ComboBox cmbMedicine;

        private Guna2HtmlLabel lblStockInfo;

        private Guna2HtmlLabel lblCustomer;
        private Guna2ComboBox cmbCustomer;

        private Guna2HtmlLabel lblQuantity;
        private Guna2NumericUpDown numQuantity;

        private Guna2Button btnSell;
        private Guna2Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblCategory = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cmbCategory = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblMedicine = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cmbMedicine = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblStockInfo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblCustomer = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cmbCustomer = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblQuantity = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.numQuantity = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.btnSell = new Guna.UI2.WinForms.Guna2Button();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.FillColor = System.Drawing.Color.White;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(480, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(186, 61);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Satış Yap";
            // 
            // lblCategory
            // 
            this.lblCategory.BackColor = System.Drawing.Color.Transparent;
            this.lblCategory.Location = new System.Drawing.Point(40, 90);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(83, 27);
            this.lblCategory.TabIndex = 1;
            this.lblCategory.Text = "Kategori";
            // 
            // cmbCategory
            // 
            this.cmbCategory.BackColor = System.Drawing.Color.Transparent;
            this.cmbCategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FocusedColor = System.Drawing.Color.Empty;
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbCategory.ItemHeight = 30;
            this.cmbCategory.Location = new System.Drawing.Point(160, 85);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(260, 36);
            this.cmbCategory.TabIndex = 2;
            // 
            // lblMedicine
            // 
            this.lblMedicine.BackColor = System.Drawing.Color.Transparent;
            this.lblMedicine.Location = new System.Drawing.Point(40, 145);
            this.lblMedicine.Name = "lblMedicine";
            this.lblMedicine.Size = new System.Drawing.Size(36, 27);
            this.lblMedicine.TabIndex = 3;
            this.lblMedicine.Text = "İlaç";
            // 
            // cmbMedicine
            // 
            this.cmbMedicine.BackColor = System.Drawing.Color.Transparent;
            this.cmbMedicine.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbMedicine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMedicine.FocusedColor = System.Drawing.Color.Empty;
            this.cmbMedicine.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbMedicine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbMedicine.ItemHeight = 30;
            this.cmbMedicine.Location = new System.Drawing.Point(160, 140);
            this.cmbMedicine.Name = "cmbMedicine";
            this.cmbMedicine.Size = new System.Drawing.Size(260, 36);
            this.cmbMedicine.TabIndex = 4;
            // 
            // lblStockInfo
            // 
            this.lblStockInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblStockInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStockInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblStockInfo.Location = new System.Drawing.Point(160, 180);
            this.lblStockInfo.Name = "lblStockInfo";
            this.lblStockInfo.Size = new System.Drawing.Size(77, 34);
            this.lblStockInfo.TabIndex = 5;
            this.lblStockInfo.Text = "Stok: -";
            // 
            // lblCustomer
            // 
            this.lblCustomer.BackColor = System.Drawing.Color.Transparent;
            this.lblCustomer.Location = new System.Drawing.Point(40, 205);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(74, 27);
            this.lblCustomer.TabIndex = 6;
            this.lblCustomer.Text = "Müşteri";
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.BackColor = System.Drawing.Color.Transparent;
            this.cmbCustomer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FocusedColor = System.Drawing.Color.Empty;
            this.cmbCustomer.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbCustomer.ItemHeight = 30;
            this.cmbCustomer.Location = new System.Drawing.Point(160, 200);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(260, 36);
            this.cmbCustomer.TabIndex = 7;
            // 
            // lblQuantity
            // 
            this.lblQuantity.BackColor = System.Drawing.Color.Transparent;
            this.lblQuantity.Location = new System.Drawing.Point(40, 260);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(47, 27);
            this.lblQuantity.TabIndex = 8;
            this.lblQuantity.Text = "Adet";
            // 
            // numQuantity
            // 
            this.numQuantity.BackColor = System.Drawing.Color.Transparent;
            this.numQuantity.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numQuantity.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numQuantity.Location = new System.Drawing.Point(160, 255);
            this.numQuantity.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.numQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(120, 36);
            this.numQuantity.TabIndex = 9;
            this.numQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnSell
            // 
            this.btnSell.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSell.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSell.ForeColor = System.Drawing.Color.White;
            this.btnSell.Location = new System.Drawing.Point(160, 310);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(120, 40);
            this.btnSell.TabIndex = 10;
            this.btnSell.Text = "Satış Yap";
            this.btnSell.Click += new System.EventHandler(this.btnSell_Click);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // 
            // btnCancel
            // 
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(300, 310);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "İptal";
            // 
            // PharmacistSale
            // 
            this.ClientSize = new System.Drawing.Size(480, 380);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.lblMedicine);
            this.Controls.Add(this.cmbMedicine);
            this.Controls.Add(this.lblStockInfo);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.cmbCustomer);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.numQuantity);
            this.Controls.Add(this.btnSell);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PharmacistSale";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Satış Yap";
            this.Load += new System.EventHandler(this.PharmacistSale_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
