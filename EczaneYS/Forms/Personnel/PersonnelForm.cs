using System;
using System.Windows.Forms;
using EczaneYS.Forms.Auth;
using EczaneYS.Services;

namespace EczaneYS.Forms.Personnel
{
    public partial class PersonnelForm : Form
    {
        public int CurrentUserId { get; private set; }
        public string CurrentRole { get; private set; }

        public PersonnelForm(int userId, string role)
        {
            InitializeComponent();

            CurrentUserId = userId;
            CurrentRole = role;
        }

        // ===============================
        // FORM LOAD
        // ===============================
        private void PersonnelForm_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "Personel Paneli";
            ApplyPermissions();
        }

        // ===============================
        // YETKİ KONTROLLERİ
        // ===============================
        private void ApplyPermissions()
        {
            btnSale.Visible = PermissionService.HasPermission("SALE_VIEW");
            btnMedicine.Visible = PermissionService.HasPermission("MEDICINE_VIEW");
            btnStock.Visible = PermissionService.HasPermission("STOCK_VIEW");
            btnCustomer.Visible = PermissionService.HasPermission("CUSTOMER_VIEW");
        }

        // ===============================
        // PANEL İÇİ FORM YÜKLEME
        // ===============================
        private void LoadContent(Form frm)
        {
            panelContent.Controls.Clear();

            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            panelContent.Controls.Add(frm);
            frm.Show();
        }

        // ===============================
        // BUTTON EVENTS (Designer ile birebir)
        // ===============================
        private void btnSale_Click(object sender, EventArgs e)
        {
            if (!PermissionService.HasPermission("SALE_VIEW"))
            {
                MessageBox.Show("Satış ekranına erişim yetkiniz yok.");
                return;
            }

            LoadContent(new PersonnelSale());
        }

        private void btnMedicine_Click(object sender, EventArgs e)
        {
            if (!PermissionService.HasPermission("MEDICINE_VIEW"))
            {
                MessageBox.Show("İlaç ekranına erişim yetkiniz yok.");
                return;
            }

            LoadContent(new Personnel_MedicineView());
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            if (!PermissionService.HasPermission("STOCK_VIEW"))
            {
                MessageBox.Show("Stok ekranına erişim yetkiniz yok.");
                return;
            }

            LoadContent(new Personnel_StockView());
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            if (!PermissionService.HasPermission("CUSTOMER_VIEW"))
            {
                MessageBox.Show("Müşteri ekranına erişim yetkiniz yok.");
                return;
            }

            LoadContent(new PersonnelCustomer());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            PermissionService.ClearCache();

            LoginForm login = new LoginForm();
            login.Show();

            this.Close();
        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
