using System;
using System.Data;
using System.Windows.Forms;
using EczaneYS.Data;
using EczaneYS.Services;

namespace EczaneYS.Forms.Personnel
{
    public partial class PersonnelSale : Form
    {
        private DataTable cartTable;

        public PersonnelSale()
        {
            InitializeComponent();
        }

        // ================= LOAD =================
        private void PersonnelSale_Load(object sender, EventArgs e)
        {
            // 🔐 Satış görüntüleme
            if (!PermissionService.HasPermission("SALE_VIEW"))
            {
                MessageBox.Show("Satışları görüntüleme yetkiniz yok.");
                this.Close();
                return;
            }

            bool canCreateSale = PermissionService.HasPermission("SALE_CREATE");

            // 🔐 Satış oluşturma paneli
            panelSaleCreate.Visible = canCreateSale;

            // 🔒 Sepet ve buton güvenliği
            btnAddToCart.Enabled = canCreateSale;
            btnCompleteSale.Enabled = canCreateSale;

            LoadMedicines();
            LoadSales();
            InitCart();
        }

        // ================= SATIŞ LİSTESİ =================
        private void LoadSales()
        {
            dgvSales.DataSource = DBHelper.GetDataTable(@"
                SELECT
                    s.satis_id,
                    s.tarih,
                    s.toplam_fiyat
                FROM satis s
                ORDER BY s.tarih DESC
            ");
        }

        // ================= İLAÇLAR =================
        private void LoadMedicines()
        {
            cmbMedicine.DataSource = DBHelper.GetDataTable(@"
                SELECT ilac_id, ad
                FROM ilac
                WHERE aktif = TRUE
                ORDER BY ad
            ");

            cmbMedicine.DisplayMember = "ad";
            cmbMedicine.ValueMember = "ilac_id";
        }

        // ================= SEPET =================
        private void InitCart()
        {
            cartTable = new DataTable();
            cartTable.Columns.Add("ilac_id", typeof(int));
            cartTable.Columns.Add("ilac", typeof(string));
            cartTable.Columns.Add("adet", typeof(int));
            cartTable.Columns.Add("fiyat", typeof(decimal));
            cartTable.Columns.Add("toplam", typeof(decimal));

            dgvCart.DataSource = cartTable;

            dgvCart.ReadOnly = true;
            dgvCart.AllowUserToAddRows = false;
            dgvCart.AllowUserToDeleteRows = false;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            UpdateTotal();
        }

        // ================= SEPETE EKLE =================
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (!PermissionService.HasPermission("SALE_CREATE"))
            {
                MessageBox.Show("Satış oluşturma yetkiniz yok.");
                return;
            }

            if (cmbMedicine.SelectedValue == null) return;

            int ilacId = Convert.ToInt32(cmbMedicine.SelectedValue);
            int adet = (int)numQty.Value;

            DataTable dt = DBHelper.GetDataTable(@"
                SELECT ad, fiyat, stok
                FROM ilac
                WHERE ilac_id=@id",
                "@id", ilacId);

            if (dt.Rows.Count == 0) return;

            int stok = Convert.ToInt32(dt.Rows[0]["stok"]);
            if (adet > stok)
            {
                MessageBox.Show("Yetersiz stok!");
                return;
            }

            decimal fiyat = Convert.ToDecimal(dt.Rows[0]["fiyat"]);
            string ad = dt.Rows[0]["ad"].ToString();

            cartTable.Rows.Add(ilacId, ad, adet, fiyat, adet * fiyat);
            UpdateTotal();
        }

        // ================= TOPLAM =================
        private void UpdateTotal()
        {
            decimal total = 0;
            foreach (DataRow r in cartTable.Rows)
                total += Convert.ToDecimal(r["toplam"]);

            lblTotal.Text = $"Toplam: {total:N2} ₺";
        }

        // ================= SATIŞI TAMAMLA =================
        private void btnCompleteSale_Click(object sender, EventArgs e)
        {
            if (!PermissionService.HasPermission("SALE_CREATE"))
            {
                MessageBox.Show("Satış oluşturma yetkiniz yok.");
                return;
            }

            if (cartTable.Rows.Count == 0)
            {
                MessageBox.Show("Sepet boş.");
                return;
            }

            try
            {
                foreach (DataRow r in cartTable.Rows)
                {
                    DBHelper.ExecuteNonQuery(@"
                        CALL sp_satis_yap(
                            @musteri,
                            @kullanici,
                            @ilac,
                            @adet
                        )",
                        "@musteri", DBNull.Value,           // PERSONNEL: müşteri opsiyonel
                        "@kullanici", AuthContext.UserId,
                        "@ilac", r["ilac_id"],
                        "@adet", r["adet"]
                    );
                }

                MessageBox.Show("Satış tamamlandı ✅");

                cartTable.Clear();
                UpdateTotal();
                LoadSales();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Satış sırasında hata oluştu:\n" + ex.Message);
            }
        }

        // ================= KAPAT =================
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
