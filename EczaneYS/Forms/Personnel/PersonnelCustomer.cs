using System;
using System.Data;
using System.Windows.Forms;
using EczaneYS.Services;
using EczaneYS.Data;

namespace EczaneYS.Forms.Personnel
{
    public partial class PersonnelCustomer : Form
    {
        public PersonnelCustomer()
        {
            InitializeComponent();
        }

        private void PersonnelCustomer_Load(object sender, EventArgs e)
        {
            // 🔐 Yetki kontrolü
            if (!PermissionService.HasPermission("CUSTOMER_VIEW"))
            {
                MessageBox.Show("Müşterileri görüntüleme yetkiniz yok.");
                this.Close();
                return;
            }

            SetupGrid();
            LoadCustomers();
        }

        // ===============================
        // GRID AYARLARI
        // ===============================
        private void SetupGrid()
        {
            dgvCustomers.ReadOnly = true;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.AllowUserToResizeRows = false;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.MultiSelect = false;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // ===============================
        // MÜŞTERİLERİ YÜKLE (SADECE GÖRÜNTÜLE)
        // ===============================
        private void LoadCustomers()
        {
            try
            {
                string query = @"
                    SELECT
                        musteri_id,
                        ad,
                        soyad,
                        telefon
                    FROM musteri
                    WHERE aktif = TRUE
                    ORDER BY ad, soyad;
                ";

                DataTable dt = DBHelper.GetDataTable(query);
                dgvCustomers.DataSource = dt;

                dgvCustomers.Columns["musteri_id"].HeaderText = "ID";
                dgvCustomers.Columns["ad"].HeaderText = "Ad";
                dgvCustomers.Columns["soyad"].HeaderText = "Soyad";
                dgvCustomers.Columns["telefon"].HeaderText = "Telefon";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Müşteriler yüklenirken hata oluştu:\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ===============================
        // YENİLE
        // ===============================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        // ===============================
        // KAPAT
        // ===============================
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}