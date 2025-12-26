using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;
using EczaneYS.Data;
using EczaneYS.Services;

namespace EczaneYS.Forms.Pharmacist
{
    public partial class PharmacistSale : Form
    {
        private readonly int _currentUserId;
        private int _currentIlacId = -1;
        private int _currentStock = 0;

        public PharmacistSale(int currentUserId)
        {
            InitializeComponent();
            _currentUserId = currentUserId;
        }

        // =========================
        // FORM LOAD
        // =========================
        private void PharmacistSale_Load(object sender, EventArgs e)
        {
            if (!PermissionService.HasPermission("SALE_CREATE"))
            {
                MessageBox.Show("Satış yapma yetkiniz yok.");
                Close();
                return;
            }

            LoadCategories();
            LoadCustomers();

            btnSell.Enabled = false;
            lblStockInfo.Text = "Stok: -";

            cmbCategory.SelectedIndexChanged += CmbCategory_SelectedIndexChanged;
            cmbMedicine.SelectedIndexChanged += CmbMedicine_SelectedIndexChanged;
            numQuantity.ValueChanged += (s, ev) => ValidateStock();
        }

        // =========================
        // KATEGORİ
        // =========================
        private void LoadCategories()
        {
            DataTable dt = DBHelper.GetDataTable(
                "SELECT kategori_id, kategori_adi FROM kategori ORDER BY kategori_adi");

            cmbCategory.DataSource = dt;
            cmbCategory.DisplayMember = "kategori_adi";
            cmbCategory.ValueMember = "kategori_id";
            cmbCategory.SelectedIndex = -1;
        }

        private void CmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMedicine.DataSource = null;
            lblStockInfo.Text = "Stok: -";
            btnSell.Enabled = false;

            if (cmbCategory.SelectedIndex == -1) return;

            int kategoriId = Convert.ToInt32(cmbCategory.SelectedValue);

            DataTable dt = DBHelper.GetDataTable(@"
                SELECT ilac_id, ad
                FROM ilac
                WHERE aktif = TRUE
                  AND kategori_id = @kid
                ORDER BY ad",
                "@kid", kategoriId
            );

            cmbMedicine.DataSource = dt;
            cmbMedicine.DisplayMember = "ad";
            cmbMedicine.ValueMember = "ilac_id";
            cmbMedicine.SelectedIndex = -1;
        }

        // =========================
        // İLAÇ SEÇİLİNCE
        // =========================
        private void CmbMedicine_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSell.Enabled = false;
            lblStockInfo.Text = "Stok: -";

            if (cmbMedicine.SelectedIndex == -1) return;

            DataRowView row = cmbMedicine.SelectedItem as DataRowView;
            if (row == null) return;

            _currentIlacId = Convert.ToInt32(row["ilac_id"]);

            object stokObj = DBHelper.ExecuteScalar(
                "SELECT stok FROM ilac WHERE ilac_id=@id",
                "@id", _currentIlacId
            );

            _currentStock = stokObj == null ? 0 : Convert.ToInt32(stokObj);

            lblStockInfo.Text = $"Stok: {_currentStock}";
            ValidateStock();
        }

        // =========================
        // STOK KONTROL
        // =========================
        private void ValidateStock()
        {
            if (_currentIlacId <= 0)
            {
                btnSell.Enabled = false;
                return;
            }

            int adet = Convert.ToInt32(numQuantity.Value);

            if (_currentStock <= 0)
            {
                btnSell.Enabled = false;
                btnSell.Text = "Stok Yok ❌";
                lblStockInfo.ForeColor = System.Drawing.Color.Red;
            }
            else if (adet > _currentStock)
            {
                btnSell.Enabled = false;
                btnSell.Text = $"Yetersiz Stok ({_currentStock})";
                lblStockInfo.ForeColor = System.Drawing.Color.OrangeRed;
            }
            else
            {
                btnSell.Enabled = true;
                btnSell.Text = "Satış Yap";
                lblStockInfo.ForeColor = System.Drawing.Color.Green;
            }
        }

        // =========================
        // MÜŞTERİ
        // =========================
        private void LoadCustomers()
        {
            DataTable dt = DBHelper.GetDataTable(@"
                SELECT musteri_id, ad || ' ' || soyad AS ad_soyad
                FROM musteri
                WHERE aktif = TRUE
                ORDER BY ad");

            cmbCustomer.DataSource = dt;
            cmbCustomer.DisplayMember = "ad_soyad";
            cmbCustomer.ValueMember = "musteri_id";
            cmbCustomer.SelectedIndex = -1;
        }

        // =========================
        // SATIŞ
        // =========================
        private void btnSell_Click(object sender, EventArgs e)
        {
            if (_currentIlacId <= 0 || cmbCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("Eksik seçim var.");
                return;
            }

            int musteriId = Convert.ToInt32(cmbCustomer.SelectedValue);
            int adet = Convert.ToInt32(numQuantity.Value);

            try
            {
                using (var conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand(
                        "CALL sp_satis_yap(@m,@k,@i,@a)", conn))
                    {
                        cmd.Parameters.AddWithValue("@m", musteriId);
                        cmd.Parameters.AddWithValue("@k", _currentUserId);
                        cmd.Parameters.AddWithValue("@i", _currentIlacId);
                        cmd.Parameters.AddWithValue("@a", adet);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Satış başarıyla tamamlandı ✅");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Satış hatası:\n" + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
