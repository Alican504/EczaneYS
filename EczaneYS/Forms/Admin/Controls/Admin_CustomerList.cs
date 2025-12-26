using System;
using System.Data;
using System.Windows.Forms;
using EczaneYS.Data;
using EczaneYS.Services;
using EczaneYS.Forms.Admin;

namespace EczaneYS.Forms.Admin.Controls
{
    public partial class Admin_CustomerList : UserControl
    {
        public Admin_CustomerList()
        {
            InitializeComponent();

            if (!PermissionService.HasPermission("CUSTOMER_VIEW"))
            {
                MessageBox.Show("Müşteri görüntüleme yetkiniz yok.");
                this.Enabled = false;
                return;
            }

            ApplyPermissions();
            LoadCustomers();
        }

        private void ApplyPermissions()
        {
            btnAdd.Visible = PermissionService.HasPermission("CUSTOMER_ADD");
            btnEdit.Visible = PermissionService.HasPermission("CUSTOMER_UPDATE");
            btnDelete.Visible = PermissionService.HasPermission("CUSTOMER_DELETE");
        }

        private void LoadCustomers()
        {
            dgvCustomers.DataSource = DBHelper.GetDataTable(@"
        SELECT
            musteri_id,
            ad || ' ' || soyad AS ad_soyad,
            telefon,
            tc_no,
            aktif
        FROM musteri
        ORDER BY ad, soyad
    ");
        }


        // ===============================
        // EKLE
        // ===============================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var dashboard = this.FindForm() as AdminDashboard;
            dashboard?.LoadContent(new Admin_AddCustomer());
        }

        // ===============================
        // GÜNCELLE
        // ===============================
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow == null) return;

            int id = Convert.ToInt32(
                dgvCustomers.CurrentRow.Cells["musteri_id"].Value
            );

            var dashboard = this.FindForm() as AdminDashboard;
            dashboard?.LoadContent(new Admin_EditCustomer(id));
        }

        // ===============================
        // SİL
        // ===============================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow == null) return;

            int id = Convert.ToInt32(
                dgvCustomers.CurrentRow.Cells["musteri_id"].Value
            );

            if (MessageBox.Show("Müşteri silinsin mi?", "Onay",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DBHelper.ExecuteNonQuery(
                    "DELETE FROM musteri WHERE musteri_id=@id",
                    "@id", id
                );

                LoadCustomers();
            }
        }
    }
}
