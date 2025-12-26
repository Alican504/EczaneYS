using System;
using System.Data;
using System.Windows.Forms;
using EczaneYS.Data;
using EczaneYS.Services;

namespace EczaneYS.Forms.Pharmacist
{
    public partial class Pharmacist_SaleHistory : Form
    {
        public Pharmacist_SaleHistory()
        {
            InitializeComponent();
        }

        // ===============================
        // FORM LOAD
        // ===============================
        private void Pharmacist_SaleHistory_Load(object sender, EventArgs e)
        {
            // 🔐 YETKİ KONTROLÜ
            if (!PermissionService.HasPermission("SALE_VIEW"))
            {
                MessageBox.Show(
                    "Satış geçmişini görüntüleme yetkiniz yok.",
                    "Yetkisiz Erişim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                this.Close();
                return;
            }

            SetupGrid();
            LoadSales();
        }

        // ===============================
        // GRID AYARLARI
        // ===============================
        private void SetupGrid()
        {
            dgvSales.ReadOnly = true;
            dgvSales.AllowUserToAddRows = false;
            dgvSales.AllowUserToDeleteRows = false;
            dgvSales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSales.RowHeadersVisible = false;
        }

        // ===============================
        // SATIŞLARI YÜKLE
        // ===============================
        private void LoadSales()
        {
            try
            {
                DataTable dt = DBHelper.GetDataTable(@"
                    SELECT
                        s.satis_id            AS ""Satış No"",
                        s.tarih               AS ""Tarih"",
                        COALESCE(m.ad || ' ' || m.soyad, '-') AS ""Müşteri"",
                        COALESCE(u.ad || ' ' || u.soyad, '-') AS ""Eczacı"",
                        s.toplam_fiyat         AS ""Toplam Tutar (₺)""
                    FROM satis s
                    LEFT JOIN musteri m   ON m.musteri_id = s.musteri_id
                    LEFT JOIN kullanici u ON u.kullanici_id = s.kullanici_id
                    ORDER BY s.tarih DESC
                ");

                dgvSales.DataSource = dt;

                dgvSales.Columns["Toplam Tutar (₺)"].DefaultCellStyle.Format = "N2";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Satışlar yüklenirken hata oluştu:\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ===============================
        // SATIŞ DETAYI (DOUBLE CLICK)
        // ===============================
        private void dgvSales_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int satisId = Convert.ToInt32(
                dgvSales.Rows[e.RowIndex].Cells["Satış No"].Value
            );

            using (var f = new Pharmacist_SaleDetail(satisId))
            {
                f.ShowDialog();
            }
        }

        // ===============================
        // YENİLE
        // ===============================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSales();
        }

        // ===============================
        // KAPAT
        // ===============================
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvSales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
