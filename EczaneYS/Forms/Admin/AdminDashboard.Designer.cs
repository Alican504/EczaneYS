using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EczaneYS.Forms.Admin
{
    partial class AdminDashboard
    {
        private System.ComponentModel.IContainer components = null;

        // ===============================
        // PANELS
        // ===============================
        private Guna2Panel panelMenu;
        private Guna2Panel panelMain;
        private Guna2Panel panelHeader;
        private Guna2Panel panelContent;

        private FlowLayoutPanel menuContainer;

        // ===============================
        // HEADER
        // ===============================
        private Label lblTitle;

        // ===============================
        // MENU BUTTON FIELDS (ONLY DECLARATION)
        // ===============================
        private Guna2Button btnDashboard;
        private Guna2Button btnUsers;
        private Guna2Button btnCustomers;
        private Guna2Button btnMedicines;
        private Guna2Button btnCategories;
        private Guna2Button btnSuppliers;
        private Guna2Button btnStock;
        private Guna2Button btnSales;
        private Guna2Button btnReports;
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
            this.menuContainer = new FlowLayoutPanel();
            this.panelMain = new Guna2Panel();
            this.panelContent = new Guna2Panel();
            this.panelHeader = new Guna2Panel();
            this.lblTitle = new Label();

            this.panelMenu.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();

            // panelMenu
            this.panelMenu.Dock = DockStyle.Left;
            this.panelMenu.FillColor = Color.FromArgb(245, 246, 250);
            this.panelMenu.Size = new Size(220, 700);
            this.panelMenu.Controls.Add(this.menuContainer);

            // menuContainer
            this.menuContainer.Dock = DockStyle.Fill;
            this.menuContainer.FlowDirection = FlowDirection.TopDown;
            this.menuContainer.WrapContents = false;
            this.menuContainer.AutoScroll = true;
            this.menuContainer.Padding = new Padding(10);

            // panelMain
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.Controls.Add(this.panelContent);
            this.panelMain.Controls.Add(this.panelHeader);

            // panelHeader
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Height = 70;
            this.panelHeader.FillColor = Color.White;
            this.panelHeader.Controls.Add(this.lblTitle);

            // lblTitle
            this.lblTitle.Dock = DockStyle.Fill;
            this.lblTitle.Text = "Dashboard";
            this.lblTitle.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // panelContent
            this.panelContent.Dock = DockStyle.Fill;
            this.panelContent.FillColor = Color.FromArgb(245, 246, 250);
            this.panelContent.Paint += new PaintEventHandler(this.panelContent_Paint);

            // AdminDashboard
            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new Size(1200, 700);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelMenu);
            this.Text = "Admin Dashboard";
            this.WindowState = FormWindowState.Maximized;
            this.Load += new EventHandler(this.AdminDashboard_Load);

            this.panelMenu.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
