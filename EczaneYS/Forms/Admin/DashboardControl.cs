using System;
using System.Windows.Forms;
using EczaneYS.Data;
using EczaneYS.Services;

namespace EczaneYS.Forms.Admin.Controls
{
    public partial class DashboardControl : UserControl
    {
        public DashboardControl()
        {
            InitializeComponent();

            // 🔐 Dashboard herkes görebilir ama yine de kontrol edelim
            if (!PermissionService.HasPermission("DASHBOARD_VIEW"))
            {
                MessageBox.Show(
                    "Dashboard görüntüleme yetkiniz yok.",
                    "Yetki Yok",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                this.Enabled = false;
                return;
            }

            LoadCounts();
        }

        // =========================
        // DASHBOARD SAYILARI
        // =========================
        private void LoadCounts()
        {
            try
            {
                valMedicines.Text = GetCountSafe(
                    "SELECT COUNT(*) FROM ilac"
                );

                valCategories.Text = GetCountSafe(
                    "SELECT COUNT(*) FROM kategori"
                );

                valUsers.Text = GetCountSafe(
                    "SELECT COUNT(*) FROM kullanici"
                );

                valSuppliers.Text = GetCountSafe(
                    "SELECT COUNT(*) FROM tedarikci"
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Dashboard verileri yüklenirken hata oluştu:\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        // =========================
        // GÜVENLİ COUNT OKUMA
        // =========================
        private string GetCountSafe(string sql)
        {
            try
            {
                object result = DBHelper.ExecuteScalar(sql);

                if (result == null || result == DBNull.Value)
                    return "0";

                return Convert.ToString(result);
            }
            catch
            {
                // Tek bir kart hata verirse tüm dashboard çökmesin
                return "0";
            }
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
            // Şu an gerek yok, ileride grafik vs. çizilebilir
        }
    }
}
