# EczaneYS

Bu proje, bir eczane otomasyon sisteminin temel işlevlerini kapsayan masaüstü tabanlı bir uygulamadır. 

Sistem; kullanıcı, rol ve yetkilendirme yönetimi, ilaç ve stok takibi, satış işlemleri ve veritabanı 

bütünlüğünü sağlayan tetikleyici (trigger) ve saklı yordam (stored procedure) yapıları içermektedir.



\## Kullanılan Teknolojiler

\- C# (.NET)

\- PostgreSQL

\- Npgsql

\- GitHub



\## Temel Özellikler

\- Rol ve yetki tabanlı kullanıcı yönetimi

\- İlaç, kategori ve tedarikçi yönetimi

\- Stok giriş–çıkış takibi

\- Satış ve satış detay işlemleri

\- Düşük stok uyarı mekanizması

\- İlaç fiyat değişim geçmişi takibi

\- Trigger ve stored procedure kullanımı ile veri bütünlüğü



\## Kurulum Adımları



1\. Bu repository’i bilgisayarınıza klonlayın.

2\. `appsettings.example.json` dosyasını kopyalayın.

3\. Kopyalanan dosyanın adını `appsettings.json` olarak değiştirin.

4\. PostgreSQL bağlantı bilgilerinizi (`Host`, `Database`, `Username`, `Password`) girin.

5\. Veritabanını oluşturmak için `schema.sql,function\_triggers.sql,seed.sql` dosyasını PostgreSQL üzerinde çalıştırın.

6\. Projeyi Visual Studio ile açarak çalıştırın.



Veritabanı bağlantı bilgileri güvenlik nedeniyle repository içerisinde tutulmamaktadır. 

Gerçek bağlantı bilgileri yalnızca yerel ortamda kullanılan `appsettings.json` dosyasında yer almakta olup, 

bu dosya `.gitignore` aracılığıyla GitHub dışına çıkarılmıştır.





