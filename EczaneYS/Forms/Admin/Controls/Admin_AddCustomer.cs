using EczaneYS.Data;
using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EczaneYS.Forms.Admin.Controls
{
    public partial class Admin_AddCustomer : UserControl
    {
        public Admin_AddCustomer()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 1️⃣ Boş alan kontrolü (4 kutu)
            if (string.IsNullOrWhiteSpace(txtAd.Text) ||
                string.IsNullOrWhiteSpace(txtSoyad.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtTcNo.Text))
            {
                MessageBox.Show("Ad, Soyad, Telefon ve TC No boş olamaz.");
                return;
            }

            // 2️⃣ TC No kontrolü (11 hane + sadece rakam)
            if (txtTcNo.Text.Length != 11 || !long.TryParse(txtTcNo.Text, out _))
            {
                MessageBox.Show("TC No 11 haneli ve sadece rakamlardan oluşmalıdır.");
                return;
            }


            DBHelper.ExecuteNonQuery(@"
        INSERT INTO musteri (ad, soyad, telefon, tc_no, aktif)
        VALUES (@ad, @soyad, @tel, @tc, @aktif)
    ",
            "@ad", txtAd.Text.Trim(),
            "@soyad", txtSoyad.Text.Trim(),
            "@tel", txtPhone.Text.Trim(),
            "@tc", txtTcNo.Text.Trim(),
            "@aktif", true
            );

            MessageBox.Show("Müşteri eklendi.");

            txtAd.Clear();
            txtSoyad.Clear();
            txtPhone.Clear();
            txtTcNo.Clear();
        }


    }
}
