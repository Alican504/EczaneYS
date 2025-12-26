using System;
using System.Data;
using System.Windows.Forms;
using EczaneYS.Services;
using EczaneYS.Data;

namespace EczaneYS.Forms.Personnel
{
    public partial class Personnel_StockView : Form
    {
        public Personnel_StockView()
        {
            InitializeComponent();
        }

        private void Personnel_StockView_Load(object sender, EventArgs e)
        {
            // 🔐 Yetki kontrolü
            if (!PermissionService.HasPermission("STOCK_VIEW"))
            {
                MessageBox.Show("Bu ekranı görüntüleme yetkiniz yok.");
                this.Close();
                return;
            }

            SetupGrid();
            LoadStock();
        }

        // ===============================
        // GRID AYARLARI (SADECE OKUMA)
        // ===============================
        private void SetupGrid()
        {
            dgvStock.ReadOnly = true;
            dgvStock.AllowUserToAddRows = false;
            dgvStock.AllowUserToDeleteRows = false;
            dgvStock.AllowUserToResizeRows = false;
            dgvStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStock.MultiSelect = false;
            dgvStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // ===============================
        // STOK LİSTESİ (VIEW YOK → TABLE)
        // ===============================
        private void LoadStock()
        {
            try
            {
                string query = @"
                    SELECT
                        ilac_id,
                        ad AS ilac_adi,
                        stok
                    FROM ilac
                    WHERE aktif = TRUE
                    ORDER BY ad;
                ";

                DataTable dt = DBHelper.GetDataTable(query);
                dgvStock.DataSource = dt;

                // Kolon başlıklarını güzelleştir
                dgvStock.Columns["ilac_id"].HeaderText = "İlaç ID";
                dgvStock.Columns["ilac_adi"].HeaderText = "İlaç Adı";
                dgvStock.Columns["stok"].HeaderText = "Stok Miktarı";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Stok bilgileri yüklenirken hata oluştu:\n" + ex.Message
                );
            }
        }

        // ===============================
        // KAPAT
        // ===============================
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvStock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
