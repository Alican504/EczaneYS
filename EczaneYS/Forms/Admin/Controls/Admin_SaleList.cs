using EczaneYS.Data;
using EczaneYS.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace EczaneYS.Forms.Admin.Controls
{
    public partial class Admin_SaleList : UserControl
    {
        public Admin_SaleList()
        {
            InitializeComponent();

            // 🔐 Yetki kontrolü
            if (!PermissionService.HasPermission("SALE_VIEW"))
            {
                MessageBox.Show("Bu sayfaya erişim yetkiniz yok.");
                this.Enabled = false;
                return;
            }

            ApplyPermissions();
            LoadSales();
        }

        // =========================
        // YETKİYE GÖRE BUTONLAR
        // =========================
        private void ApplyPermissions()
        {
            // Admin zaten hepsine sahip
            btnDetail.Visible = true;
        }

        // =========================
        // SATIŞLARI YÜKLE
        // =========================
        private void LoadSales()
        {
            DataTable dt = DBHelper.GetDataTable(@"
                SELECT
                    s.satis_id,
                    s.tarih,
                    COALESCE(k.kullanici_adi, '-') AS kullanici,
                    s.toplam_fiyat
                FROM satis s
                LEFT JOIN kullanici k ON k.kullanici_id = s.kullanici_id
                ORDER BY s.tarih DESC
            ");

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Henüz satış kaydı bulunmamaktadır.");
            }

            dgvSales.DataSource = dt;

            dgvSales.Columns["satis_id"].HeaderText = "Satış No";
            dgvSales.Columns["tarih"].HeaderText = "Tarih";
            dgvSales.Columns["kullanici"].HeaderText = "Kullanıcı";
            dgvSales.Columns["toplam_fiyat"].HeaderText = "Toplam Tutar";

            dgvSales.Columns["toplam_fiyat"].DefaultCellStyle.Format = "N2";
            dgvSales.Columns["tarih"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";

            dgvSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSales.MultiSelect = false;
        }

        // =========================
        // DETAY GÖR
        // =========================
        private void btnDetail_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir satış seçin.");
                return;
            }

            int satisId = Convert.ToInt32(
                dgvSales.SelectedRows[0].Cells["satis_id"].Value
            );

            this.Parent.Controls.Clear();
        }

        private void dgvSales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
