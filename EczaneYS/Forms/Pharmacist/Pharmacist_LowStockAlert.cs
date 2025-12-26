using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using EczaneYS.Data;
using EczaneYS.Services;

namespace EczaneYS.Forms.Pharmacist
{
    public partial class Pharmacist_LowStockAlert : Form
    {
        public Pharmacist_LowStockAlert()
        {
            InitializeComponent();
        }

        private void Pharmacist_LowStockAlert_Load(object sender, EventArgs e)
        {
            if (!PermissionService.HasPermission("STOCK_ALERT_VIEW"))
            {
                MessageBox.Show("Bu ekranı görüntüleme yetkiniz yok.");
                this.Close();
                return;
            }

            LoadAlerts();
        }

        private void LoadAlerts()
        {
            DataTable dt = DBHelper.GetDataTable(@"
                SELECT 
                    a.alert_id        AS ""ID"",
                    i.ad              AS ""İlaç"",
                    a.stok            AS ""Mevcut Stok"",
                    i.min_stok        AS ""Min Stok"",
                    a.created_at      AS ""Tarih"",
                    a.okundu          AS ""Okundu""
                FROM low_stock_alert a
                JOIN ilac i ON i.ilac_id = a.ilac_id
                ORDER BY a.okundu, a.created_at DESC
            ");

            dgvAlerts.DataSource = dt;

            // Okunmamışları kırmızı yap
            foreach (DataGridViewRow row in dgvAlerts.Rows)
            {
                bool okundu = Convert.ToBoolean(row.Cells["Okundu"].Value);
                if (!okundu)
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }
        }

        private void btnMarkRead_Click(object sender, EventArgs e)
        {
            if (dgvAlerts.SelectedRows.Count == 0) return;

            int alertId = Convert.ToInt32(
                dgvAlerts.SelectedRows[0].Cells["ID"].Value
            );

            DBHelper.ExecuteNonQuery(
                "UPDATE low_stock_alert SET okundu = TRUE WHERE alert_id=@id",
                "@id", alertId
            );

            LoadAlerts();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAlerts();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvAlerts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
