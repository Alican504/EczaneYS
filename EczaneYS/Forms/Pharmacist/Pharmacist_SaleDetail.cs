using System;
using System.Data;
using System.Windows.Forms;
using EczaneYS.Data;
using EczaneYS.Services;

namespace EczaneYS.Forms.Pharmacist
{
    public partial class Pharmacist_SaleDetail : Form
    {
        private readonly int _satisId;

        public Pharmacist_SaleDetail(int satisId)
        {
            InitializeComponent();
            _satisId = satisId;
        }

        // =========================
        // FORM LOAD
        // =========================
        private void Pharmacist_SaleDetail_Load(object sender, EventArgs e)
        {
            // 🔐 SATIŞ GÖRÜNTÜLEME YETKİSİ
            if (!PermissionService.HasPermission("SALE_VIEW"))
            {
                MessageBox.Show(
                    "Satış detaylarını görüntüleme yetkiniz yok.",
                    "Yetkisiz Erişim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                this.Close();
                return;
            }

            SetupGrid();
            LoadDetails();
        }

        // =========================
        // GRID AYARLARI
        // =========================
        private void SetupGrid()
        {
            dgvDetail.ReadOnly = true;
            dgvDetail.AllowUserToAddRows = false;
            dgvDetail.AllowUserToDeleteRows = false;
            dgvDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetail.RowHeadersVisible = false;
        }

        // =========================
        // SATIŞ DETAYLARI
        // =========================
        private void LoadDetails()
        {
            try
            {
                DataTable dt = DBHelper.GetDataTable(@"
                    SELECT
                        i.ad                 AS ""İlaç"",
                        sd.adet              AS ""Adet"",
                        sd.birim_fiyat       AS ""Birim Fiyat (₺)"",
                        (sd.adet * sd.birim_fiyat) AS ""Toplam (₺)""
                    FROM satis_detay sd
                    JOIN ilac i ON i.ilac_id = sd.ilac_id
                    WHERE sd.satis_id = @id
                ", "@id", _satisId);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Bu satışa ait detay bulunamadı.");
                    this.Close();
                    return;
                }

                dgvDetail.DataSource = dt;

                dgvDetail.Columns["Birim Fiyat (₺)"].DefaultCellStyle.Format = "N2";
                dgvDetail.Columns["Toplam (₺)"].DefaultCellStyle.Format = "N2";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Satış detayları yüklenirken hata oluştu:\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================
        // KAPAT
        // =========================
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
