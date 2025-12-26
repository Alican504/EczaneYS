using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace EczaneYS.Forms.Pharmacist
{
    partial class Pharmacist_MedicineAdd
    {
        private Guna2TextBox txtName;
        private Guna2TextBox txtBarcode;
        private Guna2TextBox txtPrice;
        private Guna2NumericUpDown numStock;
        private Guna2CheckBox chkPrescription;
        private Guna2ComboBox cmbCategory;   // ✅ EKLENDİ
        private Guna2Button btnSave;

        private void InitializeComponent()
        {
            this.txtName = new Guna2TextBox();
            this.txtBarcode = new Guna2TextBox();
            this.txtPrice = new Guna2TextBox();
            this.numStock = new Guna2NumericUpDown();
            this.chkPrescription = new Guna2CheckBox();
            this.cmbCategory = new Guna2ComboBox(); // ✅
            this.btnSave = new Guna2Button();

            this.SuspendLayout();

            // ================= İLAÇ ADI =================
            txtName.PlaceholderText = "İlaç Adı";
            txtName.Location = new Point(30, 30);
            txtName.Width = 300;

            // ================= BARKOD =================
            txtBarcode.PlaceholderText = "Barkod";
            txtBarcode.Location = new Point(30, 80);
            txtBarcode.Width = 300;

            // ================= FİYAT =================
            txtPrice.PlaceholderText = "Fiyat";
            txtPrice.Location = new Point(30, 130);
            txtPrice.Width = 300;

            // ================= KATEGORİ (EKLENDİ) =================
            cmbCategory.Location = new Point(30, 180);
            cmbCategory.Width = 300;
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.BorderRadius = 6;

            // ================= STOK =================
            numStock.Location = new Point(30, 230);
            numStock.Width = 300;
            numStock.Maximum = 100000;

            // ================= REÇETELİ =================
            chkPrescription.Text = "Reçeteli";
            chkPrescription.Location = new Point(30, 280);

            // ================= KAYDET =================
            btnSave.Text = "Kaydet";
            btnSave.Location = new Point(100, 320);
            btnSave.Size = new Size(160, 40);
            btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // ================= FORM =================
            this.Controls.AddRange(new Control[]
            {
                txtName,
                txtBarcode,
                txtPrice,
                cmbCategory,   // ✅
                numStock,
                chkPrescription,
                btnSave
            });

            this.ClientSize = new Size(360, 390);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "İlaç Ekle";
            this.Load += new System.EventHandler(this.Pharmacist_MedicineAdd_Load);
            this.ResumeLayout(false);
        }
    }
}
