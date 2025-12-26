using EczaneYS.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace EczaneYS.Forms.Admin.Controls
{
    public partial class Admin_ReportSales : UserControl
    {
        public Admin_ReportSales()
        {
            InitializeComponent();

            // Varsayılan tarih aralığı
            dtStart.Value = DateTime.Today.AddDays(-7);
            dtEnd.Value = DateTime.Today;
        }

        // =========================
        // GETİR
        // =========================
        private void btnGetir_Click(object sender, EventArgs e)
        {
            if (dtStart.Value.Date > dtEnd.Value.Date)
            {
                MessageBox.Show(
                    "Başlangıç tarihi, bitiş tarihinden büyük olamaz.",
                    "Uyarı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            LoadSales();
        }

        // =========================
        // SATIŞLARI YÜKLE
        // =========================
        private void LoadSales()
        {
            try
            {
                string query = @"
                    SELECT
                        s.satis_id,
                        s.tarih,
                        COALESCE(m.ad || ' ' || m.soyad, '-') AS musteri,
                        COALESCE(k.ad || ' ' || k.soyad, '-') AS kullanici,
                        s.toplam_fiyat
                    FROM satis s
                    LEFT JOIN musteri m ON m.musteri_id = s.musteri_id
                    LEFT JOIN kullanici k ON k.kullanici_id = s.kullanici_id
                    WHERE s.tarih BETWEEN @start AND @end
                    ORDER BY s.tarih DESC
                ";

                DataTable dt = DBHelper.GetDataTable(
                    query,
                    "@start", dtStart.Value.Date,
                    "@end", dtEnd.Value.Date.AddDays(1).AddSeconds(-1)
                );

                dgvSales.DataSource = dt;

                // Kolon başlıkları (ilk yüklemede)
                if (dgvSales.Columns.Contains("satis_id"))
                    dgvSales.Columns["satis_id"].HeaderText = "Satış No";
                if (dgvSales.Columns.Contains("tarih"))
                    dgvSales.Columns["tarih"].HeaderText = "Tarih";
                if (dgvSales.Columns.Contains("musteri"))
                    dgvSales.Columns["musteri"].HeaderText = "Müşteri";
                if (dgvSales.Columns.Contains("kullanici"))
                    dgvSales.Columns["kullanici"].HeaderText = "Kullanıcı";
                if (dgvSales.Columns.Contains("toplam_fiyat"))
                    dgvSales.Columns["toplam_fiyat"].HeaderText = "Toplam Fiyat";

                // ================= TOPLAM =================
                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["toplam_fiyat"] != DBNull.Value)
                        total += Convert.ToDecimal(row["toplam_fiyat"]);
                }

                lblTotal.Text = $"Toplam Ciro: {total:N2} ₺";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Satış raporu yüklenirken hata oluştu.\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void dgvSales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Admin_ReportSales_Load(object sender, EventArgs e)
        {

        }
    }
}