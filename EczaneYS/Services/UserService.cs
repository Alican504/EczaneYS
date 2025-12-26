using EczaneYS.Data;
using EczaneYS.Models;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace EczaneYS.Services
{
    public class UserService
    {
        public User Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            // Şifreyi hashle
            string hashedPassword = ComputeSha256Hash(password);

            // Parametreli sorgu
            string query = @"SELECT * FROM kullanici 
                             WHERE kullanici_adi=@u AND sifre=@p AND aktif=TRUE";

            DataTable dt = DBHelper.GetDataTable(
                query,
                "@u", username
            );

            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];
                return new User
                {
                    Id = Convert.ToInt32(row["kullanici_id"]),
                    KullaniciAdi = row["kullanici_adi"].ToString(),
                    Sifre = row["sifre"].ToString(),
                    RolId = Convert.ToInt32(row["rol_id"]),
                    Ad = row["ad"].ToString(),
                    Soyad = row["soyad"].ToString(),
                    Aktif = Convert.ToBoolean(row["aktif"])
                };
            }

            return null;
        }

        // SHA256 hash hesaplama
        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
    }
}
