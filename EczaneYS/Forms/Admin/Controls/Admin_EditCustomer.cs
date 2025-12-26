using EczaneYS.Data;
using System;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EczaneYS.Forms.Admin.Controls
{
    public partial class Admin_EditCustomer : UserControl
    {
        private int _customerId;

        public Admin_EditCustomer(int customerId)
        {
            InitializeComponent();
            _customerId = customerId;
            LoadCustomer();
        }

        private void LoadCustomer()
        {
            DataTable dt = DBHelper.GetDataTable(@"
        SELECT ad, soyad, telefon, tc_no
        FROM musteri
        WHERE musteri_id=@id
    ",
            "@id", _customerId);

            if (dt.Rows.Count == 0) return;

            txtAd.Text = dt.Rows[0]["ad"].ToString();
            txtSoyad.Text = dt.Rows[0]["soyad"].ToString();
            txtPhone.Text = dt.Rows[0]["telefon"].ToString();
            txtTcNo.Text = dt.Rows[0]["tc_no"].ToString();

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAd.Text) ||
                string.IsNullOrWhiteSpace(txtSoyad.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtTcNo.Text))
            {
                MessageBox.Show("Tüm alanlar doldurulmalıdır.");
                return;
            }

            DBHelper.ExecuteNonQuery(@"
        UPDATE musteri
        SET ad=@ad,
            soyad=@soyad,
            telefon=@telefon,
            tc_no=@tc
        WHERE musteri_id=@id
    ",
                "@ad", txtAd.Text.Trim(),
                "@soyad", txtSoyad.Text.Trim(),
                "@telefon", txtPhone.Text.Trim(),
                "@tc", txtTcNo.Text.Trim(),
                "@id", _customerId
            );

            MessageBox.Show("Müşteri güncellendi.");
        }



    }
}
