namespace EczaneYS.Models
{
    public class User
    {
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public int RolId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public bool Aktif { get; set; }
    }
}
