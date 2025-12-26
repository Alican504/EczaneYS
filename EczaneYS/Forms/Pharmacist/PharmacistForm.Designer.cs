using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace EczaneYS.Forms.Pharmacist
{
    partial class PharmacistForm
    {
        private System.ComponentModel.IContainer components = null;

        private Guna2Panel panelMenu;
        private Guna2Panel panelHeader;
        private Guna2Panel panelContent;

        private Guna2HtmlLabel lblTitle;

        private Guna2Button btnSale;
        private Guna2Button btnSaleHistory;
        private Guna2Button btnSaleReturn;
        private Guna2Button btnMedicineList;
        private Guna2Button btnStock;
        private Guna2Button btnStockAdjust;
        private Guna2Button btnLogout;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelMenu = new Guna2Panel();
            this.panelHeader = new Guna2Panel();
            this.panelContent = new Guna2Panel();

            this.lblTitle = new Guna2HtmlLabel();

            this.btnSale = new Guna2Button();
            this.btnSaleHistory = new Guna2Button();
            this.btnSaleReturn = new Guna2Button();
            this.btnMedicineList = new Guna2Button();
            this.btnStock = new Guna2Button();
            this.btnStockAdjust = new Guna2Button();
            this.btnLogout = new Guna2Button();

            this.SuspendLayout();

            // ================= PANEL MENU =================
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Width = 230;
            panelMenu.FillColor = Color.FromArgb(52, 73, 94);

            int y = 30;
            int gap = 48;

            void StyleMenu(Guna2Button b, string text)
            {
                b.Text = text;
                b.Size = new Size(190, 40);
                b.Location = new Point(20, y);
                b.BorderRadius = 8;
                b.FillColor = Color.FromArgb(52, 152, 219);
                b.ForeColor = Color.White;
                b.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                panelMenu.Controls.Add(b);
                y += gap;
            }

            StyleMenu(btnSale, "🧾 Satış Yap");
            btnSale.Click += btnSale_Click;

            StyleMenu(btnSaleHistory, "📄 Satış Geçmişi");
            btnSaleHistory.Click += btnSaleHistory_Click;

            StyleMenu(btnSaleReturn, "↩ Satış İade");
            btnSaleReturn.Click += btnSaleReturn_Click;

            StyleMenu(btnMedicineList, "💊 İlaç Listesi");
            btnMedicineList.Click += btnMedicineList_Click;

            StyleMenu(btnStock, "📦 Stok Durumu");
            btnStock.Click += btnStock_Click;

            StyleMenu(btnLogout, "🚪 Çıkış");
            btnLogout.FillColor = Color.FromArgb(192, 57, 43);
            btnLogout.Click += btnLogout_Click;

            // ================= PANEL HEADER =================
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 60;
            panelHeader.FillColor = Color.White;

            lblTitle.Text = "Eczacı Paneli";
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 15);
            lblTitle.ForeColor = Color.Black;

            panelHeader.Controls.Add(lblTitle);

            // ================= PANEL CONTENT =================
            panelContent.Dock = DockStyle.Fill;
            panelContent.FillColor = Color.FromArgb(245, 246, 250);

            // ================= FORM =================
            this.Controls.Add(panelContent);
            this.Controls.Add(panelHeader);
            this.Controls.Add(panelMenu);

            this.Text = "Eczacı Paneli";
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.PharmacistForm_Load);

            this.ResumeLayout(false);
        }
    }
}
