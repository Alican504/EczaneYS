using System;
using System.Windows.Forms;
using EczaneYS.Services;
using EczaneYS.Forms.Auth;

namespace EczaneYS.Forms.Pharmacist
{
    public partial class PharmacistForm : Form
    {
        private readonly int _currentUserId;
        private readonly string _currentRole;

        private Form _activeChildForm;

        public PharmacistForm(int userId, string role)
        {
            InitializeComponent();
            _currentUserId = userId;
            _currentRole = role;
        }

        private void PharmacistForm_Load(object sender, EventArgs e)
        {
            ApplyPermissions();
            OpenWelcome();
        }

        // ===============================
        // YETKİLERE GÖRE MENÜ
        // ===============================
        private void ApplyPermissions()
        {
            // SATIŞ
            btnSale.Visible = PermissionService.HasPermission("SALE_CREATE");
            btnSaleHistory.Visible = PermissionService.HasPermission("SALE_VIEW");
            btnSaleReturn.Visible = PermissionService.HasPermission("SALE_RETURN");

            // İLAÇ
            btnMedicineList.Visible = PermissionService.HasPermission("MEDICINE_VIEW");

            // STOK
            btnStock.Visible = PermissionService.HasPermission("STOCK_VIEW");

            btnStockAdjust.Visible =
                PermissionService.HasPermission("STOCK_IN") ||
                PermissionService.HasPermission("STOCK_OUT");

            btnLogout.Visible = true;
        }

        // ===============================
        // PANELCONTENT FORM AÇ
        // ===============================
        private void OpenChildForm(Form childForm)
        {
            if (_activeChildForm != null)
                _activeChildForm.Close();

            _activeChildForm = childForm;

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelContent.Controls.Clear();
            panelContent.Controls.Add(childForm);
            childForm.Show();
        }

        // ===============================
        // KARŞILAMA
        // ===============================
        private void OpenWelcome()
        {
            lblTitle.Text = "Eczacı Paneli";
            panelContent.Controls.Clear();

            Label lbl = new Label
            {
                Text = "Hoş geldiniz 👋\n\nSol menüden bir işlem seçiniz.",
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold)
            };

            panelContent.Controls.Add(lbl);
        }

        // ===============================
        // MENÜ EVENTLERİ
        // ===============================

        // 🧾 SATIŞ YAP (MODAL)
        private void btnSale_Click(object sender, EventArgs e)
        {
            using (var f = new PharmacistSale(_currentUserId))
                f.ShowDialog();
        }

        // 📄 SATIŞ GEÇMİŞİ
        private void btnSaleHistory_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Satış Geçmişi";
            OpenChildForm(new Pharmacist_SaleHistory());
        }

        // ↩ SATIŞ İADE
        private void btnSaleReturn_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Satış İade";
            OpenChildForm(new Pharmacist_SaleReturn());
        }

        // 💊 İLAÇ LİSTESİ
        private void btnMedicineList_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "İlaç Listesi";
            OpenChildForm(new Pharmacist_MedicineView());
        }

        // 📦 STOK DURUMU
        private void btnStock_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Stok Durumu";
            OpenChildForm(new Pharmacist_StockView(_currentUserId));
        }

        // 🚪 ÇIKIŞ
        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (_activeChildForm != null)
                _activeChildForm.Close();

            PermissionService.ClearCache();
            new LoginForm().Show();
            this.Close();
        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {
        }

        private void PharmacistForm_Load_1(object sender, EventArgs e)
        {

        }

        private void PharmacistForm_Load_2(object sender, EventArgs e)
        {

        }
    }
}
