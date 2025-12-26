using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using EczaneYS.Forms.Admin.Controls;
using EczaneYS.Services;
using EczaneYS.Forms.Auth;

namespace EczaneYS.Forms.Admin
{
    public partial class AdminDashboard : Form
    {
        public int CurrentUserId { get; private set; }
        public string CurrentRole { get; private set; }

        public AdminDashboard(int userId, string role)
        {
            InitializeComponent();
            CurrentUserId = userId;
            CurrentRole = role;

            CreateMenu();
            WireMenuEvents();
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            ApplyMenuPermissions();
            OpenDashboard();
        }

        // ===============================
        // MENU BUTTON CREATOR (GUNA2)
        // ===============================
        private Guna2Button CreateMenuButton(string text)
        {
            return new Guna2Button
            {
                Text = text,
                Width = 190,
                Height = 45,
                Margin = new Padding(5),
                BorderRadius = 14,
                FillColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Animated = true,
                HoverState =
                {
                    FillColor = Color.FromArgb(0, 120, 215),
                    ForeColor = Color.White
                }
            };
        }

        // ===============================
        // CREATE MENU
        // ===============================
        private void CreateMenu()
        {
            btnDashboard = CreateMenuButton("Dashboard");
            btnUsers = CreateMenuButton("Kullanıcılar");
            btnCustomers = CreateMenuButton("Müşteriler");
            btnMedicines = CreateMenuButton("İlaçlar");
            btnCategories = CreateMenuButton("Kategoriler");
            btnSuppliers = CreateMenuButton("Tedarikçiler");
            btnStock = CreateMenuButton("Stok");
            btnSales = CreateMenuButton("Satışlar");
            btnReports = CreateMenuButton("Raporlar");
            btnLogout = CreateMenuButton("Çıkış");

            menuContainer.Controls.Clear();

            menuContainer.Controls.Add(btnDashboard);
            menuContainer.Controls.Add(btnUsers);
            menuContainer.Controls.Add(btnCustomers);
            menuContainer.Controls.Add(btnMedicines);
            menuContainer.Controls.Add(btnCategories);
            menuContainer.Controls.Add(btnSuppliers);
            menuContainer.Controls.Add(btnStock);
            menuContainer.Controls.Add(btnSales);
            menuContainer.Controls.Add(btnReports);
            menuContainer.Controls.Add(btnLogout);
        }

        // ===============================
        // MENU EVENTS
        // ===============================
        private void WireMenuEvents()
        {
            btnDashboard.Click += (s, e) => OpenDashboard();
            btnUsers.Click += (s, e) => LoadContent(new Admin_UserControl());
            btnCustomers.Click += (s, e) => LoadContent(new Admin_CustomerList());
            btnMedicines.Click += (s, e) => LoadContent(new Admin_MedicineList());
            btnCategories.Click += (s, e) => LoadContent(new Admin_CategoryList());
            btnSuppliers.Click += (s, e) => LoadContent(new Admin_SupplierList());
            btnStock.Click += (s, e) => LoadContent(new Admin_StockList());
            btnSales.Click += (s, e) => LoadContent(new Admin_SaleList());
            btnReports.Click += (s, e) => LoadContent(new Admin_ReportSales());
            btnLogout.Click += BtnLogout_Click;
        }

        // ===============================
        // PERMISSIONS
        // ===============================
        private void ApplyMenuPermissions()
        {
            btnUsers.Visible = PermissionService.HasPermission("USER_VIEW");
            btnCustomers.Visible = PermissionService.HasPermission("CUSTOMER_VIEW");
            btnMedicines.Visible = PermissionService.HasPermission("MEDICINE_VIEW");
            btnCategories.Visible = PermissionService.HasPermission("CATEGORY_VIEW");
            btnSuppliers.Visible = PermissionService.HasPermission("SUPPLIER_VIEW");
            btnStock.Visible = PermissionService.HasPermission("STOCK_VIEW");
            btnSales.Visible = PermissionService.HasPermission("SALE_VIEW");
            btnReports.Visible = PermissionService.HasPermission("REPORT_VIEW");


            btnDashboard.Visible = true;
            btnLogout.Visible = true;
        }

        // ===============================
        // DASHBOARD
        // ===============================
        private void OpenDashboard()
        {
            lblTitle.Text = "Dashboard";
            LoadContent(new DashboardControl());
        }

        // ===============================
        // CONTENT LOADER
        // ===============================
        public void LoadContent(Control control)
        {
            panelContent.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panelContent.Controls.Add(control);
        }

        // ===============================
        // LOGOUT
        // ===============================
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            PermissionService.ClearCache();
            Hide();
            new LoginForm().Show();
            Close();
        }

        public void LoadControl(UserControl control)
        {
            LoadContent(control);
        }


        private void panelContent_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
