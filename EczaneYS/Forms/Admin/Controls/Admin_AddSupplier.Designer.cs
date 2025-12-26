using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace EczaneYS.Forms.Admin.Controls
{
    partial class Admin_AddSupplier
    {
        private System.ComponentModel.IContainer components = null;

        private Guna2Panel mainPanel;

        private Label lblTitle;
        private Label lblFirmaAdi;
        private Label lblYetkili;
        private Label lblTelefon;
        private Label lblEmail;
        private Label lblAdres;

        private Guna2TextBox txtFirmaAdi;
        private Guna2TextBox txtYetkili;
        private Guna2TextBox txtTelefon;
        private Guna2TextBox txtEmail;
        private Guna2TextBox txtAdres;

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
            this.mainPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblFirmaAdi = new System.Windows.Forms.Label();
            this.txtFirmaAdi = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblYetkili = new System.Windows.Forms.Label();
            this.txtYetkili = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTelefon = new System.Windows.Forms.Label();
            this.txtTelefon = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblAdres = new System.Windows.Forms.Label();
            this.txtAdres = new Guna.UI2.WinForms.Guna2TextBox();
            this.chkAktif = new Guna.UI2.WinForms.Guna2CheckBox();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.AutoScroll = true;
            this.mainPanel.BorderRadius = 18;
            this.mainPanel.Controls.Add(this.lblTitle);
            this.mainPanel.Controls.Add(this.lblFirmaAdi);
            this.mainPanel.Controls.Add(this.txtFirmaAdi);
            this.mainPanel.Controls.Add(this.lblYetkili);
            this.mainPanel.Controls.Add(this.txtYetkili);
            this.mainPanel.Controls.Add(this.lblTelefon);
            this.mainPanel.Controls.Add(this.txtTelefon);
            this.mainPanel.Controls.Add(this.lblEmail);
            this.mainPanel.Controls.Add(this.txtEmail);
            this.mainPanel.Controls.Add(this.lblAdres);
            this.mainPanel.Controls.Add(this.txtAdres);
            this.mainPanel.Controls.Add(this.chkAktif);
            this.mainPanel.Controls.Add(this.btnSave);
            this.mainPanel.Controls.Add(this.btnCancel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.FillColor = System.Drawing.Color.White;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(40);
            this.mainPanel.Size = new System.Drawing.Size(2044, 998);
            this.mainPanel.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.lblTitle.Location = new System.Drawing.Point(40, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(444, 65);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Yeni Tedarikçi Ekle";
            // 
            // lblFirmaAdi
            // 
            this.lblFirmaAdi.AutoSize = true;
            this.lblFirmaAdi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFirmaAdi.Location = new System.Drawing.Point(40, 100);
            this.lblFirmaAdi.Name = "lblFirmaAdi";
            this.lblFirmaAdi.Size = new System.Drawing.Size(148, 37);
            this.lblFirmaAdi.TabIndex = 1;
            this.lblFirmaAdi.Text = "Firma Adı *";
            // 
            // txtFirmaAdi
            // 
            this.txtFirmaAdi.BorderRadius = 10;
            this.txtFirmaAdi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFirmaAdi.DefaultText = "";
            this.txtFirmaAdi.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.txtFirmaAdi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFirmaAdi.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.txtFirmaAdi.Location = new System.Drawing.Point(40, 130);
            this.txtFirmaAdi.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtFirmaAdi.Name = "txtFirmaAdi";
            this.txtFirmaAdi.PlaceholderText = "Firma adını giriniz";
            this.txtFirmaAdi.SelectedText = "";
            this.txtFirmaAdi.Size = new System.Drawing.Size(420, 40);
            this.txtFirmaAdi.TabIndex = 2;
            // 
            // lblYetkili
            // 
            this.lblYetkili.AutoSize = true;
            this.lblYetkili.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblYetkili.Location = new System.Drawing.Point(40, 185);
            this.lblYetkili.Name = "lblYetkili";
            this.lblYetkili.Size = new System.Drawing.Size(135, 37);
            this.lblYetkili.TabIndex = 3;
            this.lblYetkili.Text = "Yetkili Kişi";
            // 
            // txtYetkili
            // 
            this.txtYetkili.BorderRadius = 10;
            this.txtYetkili.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtYetkili.DefaultText = "";
            this.txtYetkili.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtYetkili.Location = new System.Drawing.Point(40, 215);
            this.txtYetkili.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtYetkili.Name = "txtYetkili";
            this.txtYetkili.PlaceholderText = "Yetkili kişi adı";
            this.txtYetkili.SelectedText = "";
            this.txtYetkili.Size = new System.Drawing.Size(420, 40);
            this.txtYetkili.TabIndex = 4;
            // 
            // lblTelefon
            // 
            this.lblTelefon.AutoSize = true;
            this.lblTelefon.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTelefon.Location = new System.Drawing.Point(40, 270);
            this.lblTelefon.Name = "lblTelefon";
            this.lblTelefon.Size = new System.Drawing.Size(102, 37);
            this.lblTelefon.TabIndex = 5;
            this.lblTelefon.Text = "Telefon";
            // 
            // txtTelefon
            // 
            this.txtTelefon.BorderRadius = 10;
            this.txtTelefon.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTelefon.DefaultText = "";
            this.txtTelefon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTelefon.Location = new System.Drawing.Point(40, 300);
            this.txtTelefon.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtTelefon.Name = "txtTelefon";
            this.txtTelefon.PlaceholderText = "Telefon numarası";
            this.txtTelefon.SelectedText = "";
            this.txtTelefon.Size = new System.Drawing.Size(420, 40);
            this.txtTelefon.TabIndex = 6;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEmail.Location = new System.Drawing.Point(40, 355);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(106, 37);
            this.lblEmail.TabIndex = 7;
            this.lblEmail.Text = "E-Posta";
            // 
            // txtEmail
            // 
            this.txtEmail.BorderRadius = 10;
            this.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEmail.DefaultText = "";
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmail.Location = new System.Drawing.Point(40, 385);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "ornek@mail.com";
            this.txtEmail.SelectedText = "";
            this.txtEmail.Size = new System.Drawing.Size(420, 40);
            this.txtEmail.TabIndex = 8;
            // 
            // lblAdres
            // 
            this.lblAdres.AutoSize = true;
            this.lblAdres.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAdres.Location = new System.Drawing.Point(40, 440);
            this.lblAdres.Name = "lblAdres";
            this.lblAdres.Size = new System.Drawing.Size(84, 37);
            this.lblAdres.TabIndex = 9;
            this.lblAdres.Text = "Adres";
            // 
            // txtAdres
            // 
            this.txtAdres.BorderRadius = 10;
            this.txtAdres.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAdres.DefaultText = "";
            this.txtAdres.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAdres.Location = new System.Drawing.Point(40, 470);
            this.txtAdres.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtAdres.Multiline = true;
            this.txtAdres.Name = "txtAdres";
            this.txtAdres.PlaceholderText = "Adres bilgisi";
            this.txtAdres.SelectedText = "";
            this.txtAdres.Size = new System.Drawing.Size(420, 90);
            this.txtAdres.TabIndex = 10;
            // 
            // chkAktif
            // 
            this.chkAktif.Checked = true;
            this.chkAktif.CheckedState.BorderRadius = 0;
            this.chkAktif.CheckedState.BorderThickness = 0;
            this.chkAktif.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAktif.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkAktif.Location = new System.Drawing.Point(40, 580);
            this.chkAktif.Name = "chkAktif";
            this.chkAktif.Size = new System.Drawing.Size(104, 24);
            this.chkAktif.TabIndex = 11;
            this.chkAktif.Text = "Aktif";
            this.chkAktif.UncheckedState.BorderRadius = 0;
            this.chkAktif.UncheckedState.BorderThickness = 0;
            // 
            // btnSave
            // 
            this.btnSave.BorderRadius = 14;
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(190)))));
            this.btnSave.Location = new System.Drawing.Point(40, 620);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 45);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Kaydet";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BorderRadius = 14;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnCancel.Location = new System.Drawing.Point(210, 620);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 45);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "İptal";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Admin_AddSupplier
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.mainPanel);
            this.Name = "Admin_AddSupplier";
            this.Size = new System.Drawing.Size(2044, 998);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
