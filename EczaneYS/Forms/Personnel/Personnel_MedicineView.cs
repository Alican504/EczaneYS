using System;
using System.Data;
using System.Windows.Forms;
using EczaneYS.Services;
using EczaneYS.Data;

namespace EczaneYS.Forms.Personnel
{
    public partial class Personnel_MedicineView : Form
    {
        public Personnel_MedicineView()
        {
            InitializeComponent();
        }

        private void Personnel_MedicineView_Load(object sender, EventArgs e)
        {
            // 🔐 Yetki kontrolü
            if (!PermissionService.HasPermission("MEDICINE_VIEW"))
            {
                MessageBox.Show("Bu ekranı görüntüleme yetkiniz yok.");
                this.Close();
                return;
            }

            SetupGrid();
            LoadMedicines();
        }

        // =========================
        // GRID AYARLARI
        // =========================
        private void SetupGrid()
        {
            dgvMedicines.ReadOnly = true;
            dgvMedicines.AllowUserToAddRows = false;
            dgvMedicines.AllowUserToDeleteRows = false;
            dgvMedicines.AllowUserToResizeRows = false;
            dgvMedicines.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMedicines.MultiSelect = false;
            dgvMedicines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // =========================
        // İLAÇLARI YÜKLE (VIEW YOK)
        // =========================
        private void LoadMedicines()
        {
            try
            {
                string query = @"
                    SELECT
                        ilac_id,
                        ad,
                        fiyat,
                        stok
                    FROM ilac
                    WHERE aktif = TRUE
                    ORDER BY ad;
                ";

                DataTable dt = DBHelper.GetDataTable(query);
                dgvMedicines.DataSource = dt;

                // Kolon başlıklarını düzenle
                dgvMedicines.Columns["ilac_id"].HeaderText = "İlaç ID";
                dgvMedicines.Columns["ad"].HeaderText = "İlaç Adı";
                dgvMedicines.Columns["fiyat"].HeaderText = "Fiyat (₺)";
                dgvMedicines.Columns["stok"].HeaderText = "Stok";
            }
            catch (Exception ex)
            {
                MessageBox.Show("İlaçlar yüklenirken hata oluştu:\n" + ex.Message);
            }
        }

        // =========================
        // KAPAT
        // =========================
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
