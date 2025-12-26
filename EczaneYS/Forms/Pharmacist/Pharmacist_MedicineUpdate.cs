using EczaneYS.Data;
using EczaneYS.Services;
using System;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EczaneYS.Forms.Pharmacist
{
    public partial class Pharmacist_MedicineUpdate : Form
    {
        private int _medicineId;

        public Pharmacist_MedicineUpdate(int medicineId)
        {
            InitializeComponent();
            _medicineId = medicineId;
        }

        private void Pharmacist_MedicineUpdate_Load(object sender, EventArgs e)
        {
            if (!PermissionService.HasPermission("MEDICINE_UPDATE"))
            {
                MessageBox.Show("İlaç güncelleme yetkiniz yok.");
                this.Close();
                return;
            }

            LoadMedicine();
        }

        private void LoadMedicine()
        {
            DataTable dt = DBHelper.GetDataTable(
                "SELECT ad, barkod, stok, receteli_mi FROM ilac WHERE ilac_id=@id",
                "@id", _medicineId
            );

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("İlaç bulunamadı.");
                this.Close();
                return;
            }

            txtName.Text = dt.Rows[0]["ad"].ToString();
            txtBarcode.Text = dt.Rows[0]["barkod"].ToString();
            numStock.Value = Convert.ToInt32(dt.Rows[0]["stok"]);
            chkPrescription.Checked = Convert.ToBoolean(dt.Rows[0]["receteli_mi"]);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DBHelper.ExecuteNonQuery(@"
                UPDATE ilac
                SET ad=@ad, barkod=@barkod, stok=@stok, receteli_mi=@receteli
                WHERE ilac_id=@id",
                "@ad", txtName.Text.Trim(),
                "@barkod", txtBarcode.Text.Trim(),
                "@stok", (int)numStock.Value,
                "@receteli", chkPrescription.Checked,
                "@id", _medicineId
            );

            MessageBox.Show("İlaç güncellendi.");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
