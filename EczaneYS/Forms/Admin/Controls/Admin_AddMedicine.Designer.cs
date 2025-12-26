using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace EczaneYS.Forms.Admin.Controls
{
    partial class Admin_AddMedicine
    {
        private System.ComponentModel.IContainer components = null;

        private Guna2Panel mainPanel;

        private Label lblTitle;
        private Label lblAd;
        private Label lblBarkod;
        private Label lblKategori;
        private Label lblTedarikci;
        private Label lblFiyat;
        private Label lblStok;
        private Label lblMinStok;
        private Label lblSKT;

        private Guna2TextBox txtAd;
        private Guna2TextBox txtBarkod;

        private Guna2ComboBox cmbKategori;
        private Guna2ComboBox cmbTedarikci;

        private Guna2NumericUpDown numFiyat;
        private Guna2NumericUpDown numStok;
        private Guna2NumericUpDown numMinStok;

        private Guna2DateTimePicker dtpSonKullanma;

        private Guna2CheckBox chkReceteli;
        private Guna2CheckBox chkAktif;

        private Guna2Button btnSave;
        private Guna2Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.mainPanel = new Guna2Panel();
            this.lblTitle = new Label();

            this.lblAd = new Label();
            this.txtAd = new Guna2TextBox();

            this.lblBarkod = new Label();
            this.txtBarkod = new Guna2TextBox();

            this.lblKategori = new Label();
            this.cmbKategori = new Guna2ComboBox();

            this.lblTedarikci = new Label();
            this.cmbTedarikci = new Guna2ComboBox();

            this.lblFiyat = new Label();
            this.numFiyat = new Guna2NumericUpDown();

            this.lblStok = new Label();
            this.numStok = new Guna2NumericUpDown();

            this.lblMinStok = new Label();
            this.numMinStok = new Guna2NumericUpDown();

            this.lblSKT = new Label();
            this.dtpSonKullanma = new Guna2DateTimePicker();

            this.chkReceteli = new Guna2CheckBox();
            this.chkAktif = new Guna2CheckBox();

            this.btnSave = new Guna2Button();
            this.btnCancel = new Guna2Button();

            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFiyat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinStok)).BeginInit();
            this.SuspendLayout();

            // ================= PANEL =================
            this.mainPanel.Dock = DockStyle.Fill;
            this.mainPanel.Padding = new Padding(40);
            this.mainPanel.FillColor = Color.White;
            this.Controls.Add(this.mainPanel);

            // ================= TITLE =================
            this.lblTitle.Text = "Yeni İlaç Ekle";
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitle.Location = new Point(40, 30);
            this.lblTitle.AutoSize = true;
            this.mainPanel.Controls.Add(this.lblTitle);

            // ================= İLAÇ ADI =================
            this.lblAd.Text = "İlaç Adı";
            this.lblAd.Location = new Point(40, 100);
            this.mainPanel.Controls.Add(this.lblAd);

            this.txtAd.Location = new Point(40, 130);
            this.txtAd.Size = new Size(360, 40);
            this.mainPanel.Controls.Add(this.txtAd);

            // ================= BARKOD =================
            this.lblBarkod.Text = "Barkod";
            this.lblBarkod.Location = new Point(40, 185);
            this.mainPanel.Controls.Add(this.lblBarkod);

            this.txtBarkod.Location = new Point(40, 215);
            this.txtBarkod.Size = new Size(360, 40);
            this.mainPanel.Controls.Add(this.txtBarkod);

            // ================= KATEGORİ =================
            this.lblKategori.Text = "Kategori";
            this.lblKategori.Location = new Point(40, 270);
            this.mainPanel.Controls.Add(this.lblKategori);

            this.cmbKategori.Location = new Point(40, 300);
            this.cmbKategori.Size = new Size(360, 40);
            this.cmbKategori.DropDownStyle = ComboBoxStyle.DropDownList;
            this.mainPanel.Controls.Add(this.cmbKategori);

            // ================= TEDARİKÇİ =================
            this.lblTedarikci.Text = "Tedarikçi";
            this.lblTedarikci.Location = new Point(40, 355);
            this.mainPanel.Controls.Add(this.lblTedarikci);

            this.cmbTedarikci.Location = new Point(40, 385);
            this.cmbTedarikci.Size = new Size(360, 40);
            this.cmbTedarikci.DropDownStyle = ComboBoxStyle.DropDownList;
            this.mainPanel.Controls.Add(this.cmbTedarikci);

            // ================= FİYAT =================
            this.lblFiyat.Text = "Fiyat";
            this.lblFiyat.Location = new Point(440, 100);
            this.mainPanel.Controls.Add(this.lblFiyat);

            this.numFiyat.Location = new Point(440, 130);
            this.numFiyat.Size = new Size(260, 40);
            this.numFiyat.Minimum = 1;
            this.mainPanel.Controls.Add(this.numFiyat);

            // ================= STOK =================
            this.lblStok.Text = "Stok";
            this.lblStok.Location = new Point(440, 185);
            this.mainPanel.Controls.Add(this.lblStok);

            this.numStok.Location = new Point(440, 215);
            this.numStok.Size = new Size(260, 40);
            this.mainPanel.Controls.Add(this.numStok);

            // ================= MIN STOK =================
            this.lblMinStok.Text = "Min. Stok";
            this.lblMinStok.Location = new Point(440, 270);
            this.mainPanel.Controls.Add(this.lblMinStok);

            this.numMinStok.Location = new Point(440, 300);
            this.numMinStok.Size = new Size(260, 40);
            this.mainPanel.Controls.Add(this.numMinStok);

            // ================= SKT =================
            this.lblSKT.Text = "Son Kullanma Tarihi";
            this.lblSKT.Location = new Point(440, 355);
            this.mainPanel.Controls.Add(this.lblSKT);

            this.dtpSonKullanma.Location = new Point(440, 385);
            this.dtpSonKullanma.Size = new Size(260, 40);
            this.dtpSonKullanma.ShowCheckBox = true;
            this.mainPanel.Controls.Add(this.dtpSonKullanma);

            // ================= CHECKBOX =================
            this.chkReceteli.Text = "Reçeteli";
            this.chkReceteli.Location = new Point(40, 450);
            this.mainPanel.Controls.Add(this.chkReceteli);

            this.chkAktif.Text = "Aktif";
            this.chkAktif.Checked = true;
            this.chkAktif.Location = new Point(140, 450);
            this.mainPanel.Controls.Add(this.chkAktif);

            // ================= BUTTONS =================
            this.btnSave.Text = "Kaydet";
            this.btnSave.Location = new Point(440, 450);
            this.btnSave.Size = new Size(150, 45);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.mainPanel.Controls.Add(this.btnSave);

            this.btnCancel.Text = "İptal";
            this.btnCancel.Location = new Point(610, 450);
            this.btnCancel.Size = new Size(150, 45);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.mainPanel.Controls.Add(this.btnCancel);

            // ================= FORM =================
            this.BackColor = Color.FromArgb(245, 246, 250);
            this.Size = new Size(2044, 998);

            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFiyat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinStok)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
