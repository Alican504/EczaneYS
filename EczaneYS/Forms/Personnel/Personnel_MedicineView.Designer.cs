using System.Drawing;
using System.Windows.Forms;

namespace EczaneYS.Forms.Personnel
{
    partial class Personnel_MedicineView
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelMain = new Panel();
            this.lblTitle = new Label();
            this.dgvMedicines = new DataGridView();
            this.btnClose = new Button();

            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicines)).BeginInit();
            this.SuspendLayout();

            // 
            // panelMain
            // 
            this.panelMain.BackColor = Color.White;
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.Padding = new Padding(20);
            this.panelMain.Controls.Add(this.dgvMedicines);
            this.panelMain.Controls.Add(this.btnClose);
            this.panelMain.Controls.Add(this.lblTitle);

            // 
            // lblTitle
            // 
            this.lblTitle.Dock = DockStyle.Top;
            this.lblTitle.Height = 50;
            this.lblTitle.Text = "İlaç Listesi";
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(33, 37, 41);
            this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // dgvMedicines
            // 
            this.dgvMedicines.Dock = DockStyle.Fill;
            this.dgvMedicines.BackgroundColor = Color.White;
            this.dgvMedicines.BorderStyle = BorderStyle.None;
            this.dgvMedicines.ColumnHeadersHeight = 40;
            this.dgvMedicines.EnableHeadersVisualStyles = false;
            this.dgvMedicines.RowHeadersVisible = false;
            this.dgvMedicines.GridColor = Color.FromArgb(230, 235, 240);

            this.dgvMedicines.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255);
            this.dgvMedicines.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvMedicines.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10F, FontStyle.Bold);

            this.dgvMedicines.DefaultCellStyle.Font =
                new Font("Segoe UI", 10F);
            this.dgvMedicines.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(220, 235, 255);
            this.dgvMedicines.DefaultCellStyle.SelectionForeColor =
                Color.Black;

            // 
            // btnClose
            // 
            this.btnClose.Dock = DockStyle.Bottom;
            this.btnClose.Height = 45;
            this.btnClose.Text = "Kapat";
            this.btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnClose.BackColor = Color.FromArgb(220, 53, 69);
            this.btnClose.ForeColor = Color.White;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Margin = new Padding(0, 10, 0, 0);
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // 
            // Personnel_MedicineView
            // 
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 500);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "İlaçlar";
            this.Load += new System.EventHandler(this.Personnel_MedicineView_Load);

            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicines)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Panel panelMain;
        private Label lblTitle;
        private DataGridView dgvMedicines;
        private Button btnClose;
    }
}
