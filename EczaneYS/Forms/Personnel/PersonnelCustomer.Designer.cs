using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace EczaneYS.Forms.Personnel
{
    partial class PersonnelCustomer
    {
        private System.ComponentModel.IContainer components = null;

        private Guna2Panel panelMain;
        private Label lblTitle;
        private Guna2DataGridView dgvCustomers;
        private FlowLayoutPanel buttonPanel;
        private Guna2Button btnRefresh;
        private Guna2Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle rowStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle altRowStyle = new DataGridViewCellStyle();

            this.panelMain = new Guna2Panel();
            this.lblTitle = new Label();
            this.dgvCustomers = new Guna2DataGridView();
            this.buttonPanel = new FlowLayoutPanel();
            this.btnRefresh = new Guna2Button();
            this.btnClose = new Guna2Button();

            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();

            // ================= PANEL MAIN =================
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.FillColor = Color.White;
            this.panelMain.Padding = new Padding(20);
            this.panelMain.Controls.Add(this.dgvCustomers);
            this.panelMain.Controls.Add(this.buttonPanel);
            this.panelMain.Controls.Add(this.lblTitle);

            // ================= TITLE =================
            this.lblTitle.Dock = DockStyle.Top;
            this.lblTitle.Height = 50;
            this.lblTitle.Text = "Müşteri Listesi";
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(33, 37, 41);
            this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;

            // ================= GRID =================
            this.dgvCustomers.Dock = DockStyle.Fill;
            this.dgvCustomers.BackgroundColor = Color.White;
            this.dgvCustomers.BorderStyle = BorderStyle.None;
            this.dgvCustomers.EnableHeadersVisualStyles = false;
            this.dgvCustomers.RowHeadersVisible = false;
            this.dgvCustomers.ColumnHeadersHeight = 40;
            this.dgvCustomers.GridColor = Color.FromArgb(230, 235, 240);

            // Header
            headerStyle.BackColor = Color.FromArgb(0, 123, 255);
            headerStyle.ForeColor = Color.White;
            headerStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dgvCustomers.ColumnHeadersDefaultCellStyle = headerStyle;

            // Rows
            rowStyle.Font = new Font("Segoe UI", 10F);
            rowStyle.SelectionBackColor = Color.FromArgb(220, 235, 255);
            rowStyle.SelectionForeColor = Color.Black;
            this.dgvCustomers.DefaultCellStyle = rowStyle;

            // Alternating rows
            altRowStyle.BackColor = Color.FromArgb(248, 249, 250);
            this.dgvCustomers.AlternatingRowsDefaultCellStyle = altRowStyle;

            // ================= BUTTON PANEL =================
            this.buttonPanel.Dock = DockStyle.Bottom;
            this.buttonPanel.Height = 55;
            this.buttonPanel.Padding = new Padding(0, 10, 0, 0);
            this.buttonPanel.FlowDirection = FlowDirection.LeftToRight;

            // Refresh
            this.btnRefresh.Text = "Yenile";
            this.btnRefresh.BorderRadius = 10;
            this.btnRefresh.FillColor = Color.FromArgb(108, 117, 125);
            this.btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.Size = new Size(120, 30);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // Close
            this.btnClose.Text = "Kapat";
            this.btnClose.BorderRadius = 10;
            this.btnClose.FillColor = Color.FromArgb(220, 53, 69);
            this.btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnClose.ForeColor = Color.White;
            this.btnClose.Size = new Size(120, 30);
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            this.buttonPanel.Controls.Add(this.btnRefresh);
            this.buttonPanel.Controls.Add(this.btnClose);

            // ================= FORM =================
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 500);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Müşteriler";
            this.Load += new System.EventHandler(this.PersonnelCustomer_Load);

            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}