using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace EczaneYS.Forms.Pharmacist
{
    partial class Pharmacist_MedicineUpdate
    {
        private Guna2TextBox txtName;
        private Guna2TextBox txtBarcode;
        private Guna2NumericUpDown numStock;
        private Guna2CheckBox chkPrescription;
        private Guna2Button btnSave;

        private void InitializeComponent()
        {
            txtName = new Guna2TextBox();
            txtBarcode = new Guna2TextBox();
            numStock = new Guna2NumericUpDown();
            chkPrescription = new Guna2CheckBox();
            btnSave = new Guna2Button();

            txtName.Location = new Point(30, 30);
            txtName.Width = 300;

            txtBarcode.Location = new Point(30, 80);
            txtBarcode.Width = 300;

            numStock.Location = new Point(30, 130);
            numStock.Width = 300;

            chkPrescription.Text = "Reçeteli";
            chkPrescription.Location = new Point(30, 180);

            btnSave.Text = "Kaydet";
            btnSave.Location = new Point(100, 230);
            btnSave.Size = new Size(160, 40);
            btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.Controls.AddRange(new Control[]
            {
                txtName, txtBarcode, numStock, chkPrescription, btnSave
            });

            this.ClientSize = new Size(360, 300);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "İlaç Güncelle";
            this.Load += new System.EventHandler(this.Pharmacist_MedicineUpdate_Load);
        }
    }
}
