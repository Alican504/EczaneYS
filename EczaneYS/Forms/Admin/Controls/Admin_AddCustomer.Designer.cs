using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace EczaneYS.Forms.Admin.Controls
{
    partial class Admin_AddCustomer
    {
        private System.ComponentModel.IContainer components = null;

        private Guna2TextBox txtAd;
        private Guna2TextBox txtSoyad;
        private Guna2TextBox txtPhone;
        private Guna2TextBox txtTcNo;
        private Guna2Button btnSave;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtAd = new Guna2TextBox();
            this.txtSoyad = new Guna2TextBox();
            this.txtPhone = new Guna2TextBox();
            this.txtTcNo = new Guna2TextBox();
            this.btnSave = new Guna2Button();

            // Ad
            this.txtAd.PlaceholderText = "Ad";
            this.txtAd.Location = new Point(30, 30);
            this.txtAd.Width = 260;

            // Soyad
            this.txtSoyad.PlaceholderText = "Soyad";
            this.txtSoyad.Location = new Point(30, 80);
            this.txtSoyad.Width = 260;

            this.txtPhone.PlaceholderText = "Telefon";
            this.txtPhone.Location = new Point(30, 130);
            this.txtPhone.Width = 260;

            this.txtTcNo.PlaceholderText = "TC No";
            this.txtTcNo.Location = new Point(30, 180);
            this.txtTcNo.Width = 260;

            this.btnSave.Text = "Kaydet";
            this.btnSave.Location = new Point(30, 230);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.Controls.Add(this.txtAd);
            this.Controls.Add(this.txtSoyad);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtTcNo);
            this.Controls.Add(this.btnSave);
            this.Size = new Size(340, 260);
        }
    }
}
