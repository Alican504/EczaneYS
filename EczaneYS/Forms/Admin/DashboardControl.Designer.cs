using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EczaneYS.Forms.Admin.Controls
{
    partial class DashboardControl
    {
        private System.ComponentModel.IContainer components = null;

        private Guna2Panel mainPanel;

        private Guna2Panel cardMedicines;
        private Guna2Panel cardCategories;
        private Guna2Panel cardUsers;
        private Guna2Panel cardSuppliers;

        private Label lblTotalMedicines;
        private Label valMedicines;

        private Label lblTotalCategories;
        private Label valCategories;

        private Label lblTotalUsers;
        private Label valUsers;

        private Label lblTotalSuppliers;
        private Label valSuppliers;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.mainPanel = new Guna2Panel();

            this.cardMedicines = new Guna2Panel();
            this.lblTotalMedicines = new Label();
            this.valMedicines = new Label();

            this.cardCategories = new Guna2Panel();
            this.lblTotalCategories = new Label();
            this.valCategories = new Label();

            this.cardUsers = new Guna2Panel();
            this.lblTotalUsers = new Label();
            this.valUsers = new Label();

            this.cardSuppliers = new Guna2Panel();
            this.lblTotalSuppliers = new Label();
            this.valSuppliers = new Label();

            this.SuspendLayout();

            // ======================
            // mainPanel
            // ======================
            this.mainPanel.Dock = DockStyle.Fill;
            this.mainPanel.FillColor = Color.FromArgb(245, 246, 250);
            this.mainPanel.Padding = new Padding(30);

            // ======================
            // cardMedicines
            // ======================
            this.cardMedicines.Location = new Point(30, 30);
            this.cardMedicines.Size = new Size(260, 150);
            this.cardMedicines.BorderRadius = 22;
            this.cardMedicines.FillColor = Color.FromArgb(0, 120, 215);
            this.cardMedicines.ShadowDecoration.Enabled = true;
            this.cardMedicines.ShadowDecoration.Depth = 6;
            this.cardMedicines.ShadowDecoration.BorderRadius = 22;

            this.lblTotalMedicines.Text = "Toplam İlaç";
            this.lblTotalMedicines.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            this.lblTotalMedicines.ForeColor = Color.White;
            this.lblTotalMedicines.BackColor = Color.Transparent;
            this.lblTotalMedicines.AutoSize = true;
            this.lblTotalMedicines.Location = new Point(20, 20);

            this.valMedicines.Text = "0";
            this.valMedicines.Font = new Font("Segoe UI", 30f, FontStyle.Bold);
            this.valMedicines.ForeColor = Color.White;
            this.valMedicines.BackColor = Color.Transparent;
            this.valMedicines.AutoSize = true;
            this.valMedicines.Location = new Point(20, 60);

            this.cardMedicines.Controls.Add(this.lblTotalMedicines);
            this.cardMedicines.Controls.Add(this.valMedicines);
            this.lblTotalMedicines.BringToFront();
            this.valMedicines.BringToFront();

            // ======================
            // cardCategories
            // ======================
            this.cardCategories.Location = new Point(320, 30);
            this.cardCategories.Size = new Size(260, 150);
            this.cardCategories.BorderRadius = 22;
            this.cardCategories.FillColor = Color.FromArgb(40, 167, 69);
            this.cardCategories.ShadowDecoration.Enabled = true;
            this.cardCategories.ShadowDecoration.Depth = 6;
            this.cardCategories.ShadowDecoration.BorderRadius = 22;

            this.lblTotalCategories.Text = "Toplam Kategori";
            this.lblTotalCategories.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            this.lblTotalCategories.ForeColor = Color.White;
            this.lblTotalCategories.BackColor = Color.Transparent;
            this.lblTotalCategories.AutoSize = true;
            this.lblTotalCategories.Location = new Point(20, 20);

            this.valCategories.Text = "0";
            this.valCategories.Font = new Font("Segoe UI", 30f, FontStyle.Bold);
            this.valCategories.ForeColor = Color.White;
            this.valCategories.BackColor = Color.Transparent;
            this.valCategories.AutoSize = true;
            this.valCategories.Location = new Point(20, 60);

            this.cardCategories.Controls.Add(this.lblTotalCategories);
            this.cardCategories.Controls.Add(this.valCategories);
            this.lblTotalCategories.BringToFront();
            this.valCategories.BringToFront();

            // ======================
            // cardUsers
            // ======================
            this.cardUsers.Location = new Point(610, 30);
            this.cardUsers.Size = new Size(260, 150);
            this.cardUsers.BorderRadius = 22;
            this.cardUsers.FillColor = Color.FromArgb(255, 193, 7);
            this.cardUsers.ShadowDecoration.Enabled = true;
            this.cardUsers.ShadowDecoration.Depth = 6;
            this.cardUsers.ShadowDecoration.BorderRadius = 22;

            this.lblTotalUsers.Text = "Toplam Kullanıcı";
            this.lblTotalUsers.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            this.lblTotalUsers.ForeColor = Color.Black;
            this.lblTotalUsers.BackColor = Color.Transparent;
            this.lblTotalUsers.AutoSize = true;
            this.lblTotalUsers.Location = new Point(20, 20);

            this.valUsers.Text = "0";
            this.valUsers.Font = new Font("Segoe UI", 30f, FontStyle.Bold);
            this.valUsers.ForeColor = Color.Black;
            this.valUsers.BackColor = Color.Transparent;
            this.valUsers.AutoSize = true;
            this.valUsers.Location = new Point(20, 60);

            this.cardUsers.Controls.Add(this.lblTotalUsers);
            this.cardUsers.Controls.Add(this.valUsers);
            this.lblTotalUsers.BringToFront();
            this.valUsers.BringToFront();

            // ======================
            // cardSuppliers
            // ======================
            this.cardSuppliers.Location = new Point(900, 30);
            this.cardSuppliers.Size = new Size(260, 150);
            this.cardSuppliers.BorderRadius = 22;
            this.cardSuppliers.FillColor = Color.FromArgb(108, 117, 125);
            this.cardSuppliers.ShadowDecoration.Enabled = true;
            this.cardSuppliers.ShadowDecoration.Depth = 6;
            this.cardSuppliers.ShadowDecoration.BorderRadius = 22;

            this.lblTotalSuppliers.Text = "Toplam Tedarikçi";
            this.lblTotalSuppliers.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            this.lblTotalSuppliers.ForeColor = Color.White;
            this.lblTotalSuppliers.BackColor = Color.Transparent;
            this.lblTotalSuppliers.AutoSize = true;
            this.lblTotalSuppliers.Location = new Point(20, 20);

            this.valSuppliers.Text = "0";
            this.valSuppliers.Font = new Font("Segoe UI", 30f, FontStyle.Bold);
            this.valSuppliers.ForeColor = Color.White;
            this.valSuppliers.BackColor = Color.Transparent;
            this.valSuppliers.AutoSize = true;
            this.valSuppliers.Location = new Point(20, 60);

            this.cardSuppliers.Controls.Add(this.lblTotalSuppliers);
            this.cardSuppliers.Controls.Add(this.valSuppliers);
            this.lblTotalSuppliers.BringToFront();
            this.valSuppliers.BringToFront();

            // ======================
            // mainPanel add
            // ======================
            this.mainPanel.Controls.Add(this.cardMedicines);
            this.mainPanel.Controls.Add(this.cardCategories);
            this.mainPanel.Controls.Add(this.cardUsers);
            this.mainPanel.Controls.Add(this.cardSuppliers);

            // ======================
            // DashboardControl
            // ======================
            this.Controls.Add(this.mainPanel);
            this.BackColor = Color.FromArgb(245, 246, 250);
            this.Name = "DashboardControl";
            this.Size = new Size(2102, 1382);

            this.ResumeLayout(false);
        }
    }
}
