using EczaneYS.Data;
using EczaneYS.Services;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace EczaneYS.Forms.Admin.Controls
{
    public partial class Admin_StockList : UserControl
    {
        public Admin_StockList()
        {
            InitializeComponent();

            // 🔐 Admin her zaman yetkili (ileride role göre açılabilir)
            LoadStock();
            ApplyGridStyle();
        }

        // =========================
        // STOK LİSTESİ
        // =========================
        private void LoadStock()
        {
            DataTable dt = DBHelper.GetDataTable(@"
                SELECT
                    i.ilac_id,
                    i.ad,
                    i.stok,
                    i.min_stok,
                    CASE 
                        WHEN i.stok <= i.min_stok THEN TRUE 
                        ELSE FALSE 
                    END AS kritik
                FROM ilac i
                ORDER BY i.ad
            ");

            dgvStock.DataSource = dt;

            dgvStock.Columns["ilac_id"].Visible = false;
            dgvStock.Columns["ad"].HeaderText = "İlaç";
            dgvStock.Columns["stok"].HeaderText = "Mevcut Stok";
            dgvStock.Columns["min_stok"].HeaderText = "Min. Stok";
            dgvStock.Columns["kritik"].HeaderText = "Kritik";

            // Kritik kolonunu checkbox gibi gösterelim
            dgvStock.Columns["kritik"].ReadOnly = true;
        }

        // =========================
        // KRİTİK STOK RENKLENDİRME
        // =========================
        private void ApplyGridStyle()
        {
            dgvStock.RowPrePaint += (s, e) =>
            {
                var row = dgvStock.Rows[e.RowIndex];

                if (row.Cells["kritik"].Value != DBNull.Value &&
                    Convert.ToBoolean(row.Cells["kritik"].Value))
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            };
        }

        // =========================
        // STOK DÜZELT
        // =========================
        


        // =========================
        // YENİLE
        // =========================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStock();
        }

        private void dgvStock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bilinçli boş
        }
    }
}
