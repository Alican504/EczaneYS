using EczaneYS.Data;
using EczaneYS.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace EczaneYS.Forms.Admin.Controls
{
    public partial class Admin_SupplierList : UserControl
    {
        public Admin_SupplierList()
        {
            InitializeComponent();

            // 🔐 GÖRÜNTÜLEME YETKİSİ
            if (!PermissionService.HasPermission("SUPPLIER_VIEW"))
            {
                MessageBox.Show(
                    "Tedarikçileri görüntüleme yetkiniz yok.",
                    "Yetki Yok",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                dgv.DataSource = null;
                ApplyPermissions();
                return;
            }

            ApplyPermissions();
            LoadSuppliers();
        }

        // ===============================
        // YETKİYE GÖRE BUTONLAR
        // ===============================
        private void ApplyPermissions()
        {
            btnAdd.Visible = PermissionService.HasPermission("SUPPLIER_ADD");
            btnEdit.Visible = PermissionService.HasPermission("SUPPLIER_UPDATE");
            btnDelete.Visible = PermissionService.HasPermission("SUPPLIER_DELETE");
        }

        // ===============================
        // TEDARİKÇİLERİ YÜKLE
        // ===============================
        private void LoadSuppliers()
        {
            DataTable dt = DBHelper.GetDataTable(@"
        SELECT
            tedarikci_id,
            firma_adi,
            yetkili_kisi,
            telefon,
            adres,
            email,
            aktif
        FROM tedarikci
        ORDER BY firma_adi
    ");

            dgv.DataSource = dt;

            // ID gizle
            if (dgv.Columns.Contains("tedarikci_id"))
                dgv.Columns["tedarikci_id"].Visible = false;

            // Başlıklar
            dgv.Columns["firma_adi"].HeaderText = "Firma Adı";
            dgv.Columns["yetkili_kisi"].HeaderText = "Yetkili Kişi";
            dgv.Columns["telefon"].HeaderText = "Telefon";
            dgv.Columns["adres"].HeaderText = "Adres";
            dgv.Columns["email"].HeaderText = "E-Posta";
            dgv.Columns["aktif"].HeaderText = "Aktif";

            // Görsel iyileştirme
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            dgv.Columns["adres"].FillWeight = 200;
            dgv.Columns["email"].FillWeight = 150;
        }


        // ===============================
        // TEDARİKÇİ EKLE
        // ===============================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!PermissionService.HasPermission("SUPPLIER_ADD"))
                return;

            var dashboard = this.FindForm() as AdminDashboard;
            if (dashboard == null) return;

            dashboard.LoadControl(new Admin_AddSupplier());
        }


        // ===============================
        // AKTİF / PASİF (UPDATE)
        // ===============================
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Bir tedarikçi seçin");
                return;
            }

            int id = Convert.ToInt32(dgv.SelectedRows[0].Cells["tedarikci_id"].Value);
            bool aktif = Convert.ToBoolean(dgv.SelectedRows[0].Cells["aktif"].Value);

            DBHelper.ExecuteNonQuery(@"
                UPDATE tedarikci
                SET aktif = @a
                WHERE tedarikci_id = @id
            ",
            "@a", !aktif,
            "@id", id);

            LoadSuppliers();
        }

        // ===============================
        // SİL (HARD DELETE)
        // ===============================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Bir tedarikçi seçin");
                return;
            }

            int id = Convert.ToInt32(dgv.SelectedRows[0].Cells["tedarikci_id"].Value);

            if (MessageBox.Show(
                "Bu tedarikçiyi silmek istiyor musunuz?",
                "Sil",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            DBHelper.ExecuteNonQuery(@"
        DELETE FROM tedarikci
        WHERE tedarikci_id = @id
    ",
            "@id", id);

            LoadSuppliers();
        }


        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bilinçli olarak boş
        }

        private void dgv_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
