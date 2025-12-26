-- ===========================
-- ROLLER
-- ===========================
INSERT INTO rol (rol_kodu, rol_adi) VALUES
('ADMIN', 'Admin'),
('PHARMACIST', 'Eczacı'),
('PERSONNEL', 'Personel');

-- ===========================
-- YETKİLER
-- ===========================
INSERT INTO yetki (yetki_kodu, yetki_adi) VALUES
-- Kullanıcı & Rol
('USER_VIEW', 'Kullanıcı Görüntüleme'),
('USER_ADD', 'Kullanıcı Ekleme'),
('USER_UPDATE', 'Kullanıcı Güncelleme'),
('USER_DELETE', 'Kullanıcı Silme'),
('ROLE_VIEW', 'Rol Görüntüleme'),
('ROLE_UPDATE', 'Rol ve Yetki Atama'),

-- İlaç
('MEDICINE_VIEW', 'İlaç Görüntüleme'),
('MEDICINE_ADD', 'İlaç Ekleme'),
('MEDICINE_UPDATE', 'İlaç Güncelleme'),
('MEDICINE_DELETE', 'İlaç Silme'),
('MEDICINE_PRICE_UPDATE', 'İlaç Fiyat Güncelleme'),

-- Kategori
('CATEGORY_VIEW', 'Kategori Görüntüleme'),
('CATEGORY_ADD', 'Kategori Ekleme'),
('CATEGORY_UPDATE', 'Kategori Güncelleme'),
('CATEGORY_DELETE', 'Kategori Silme'),

-- Tedarikçi
('SUPPLIER_VIEW', 'Tedarikçi Görüntüleme'),
('SUPPLIER_ADD', 'Tedarikçi Ekleme'),
('SUPPLIER_UPDATE', 'Tedarikçi Güncelleme'),
('SUPPLIER_DELETE', 'Tedarikçi Silme'),

-- Stok
('STOCK_VIEW', 'Stok Görüntüleme'),
('STOCK_IN', 'Stok Girişi Yapma'),
('STOCK_OUT', 'Stok Çıkışı Yapma'),
('LOW_STOCK_VIEW', 'Düşük Stok Uyarılarını Görüntüleme'),

-- Satış
('SALE_CREATE', 'Satış Yapma'),
('SALE_VIEW', 'Satış Görüntüleme'),
('SALE_CANCEL', 'Satış İptali'),
('SALE_RETURN', 'Satış İade İşlemi'),

-- Müşteri
('CUSTOMER_VIEW', 'Müşteri Görüntüleme'),
('CUSTOMER_ADD', 'Müşteri Ekleme'),
('CUSTOMER_UPDATE', 'Müşteri Güncelleme'),

-- Rapor
('REPORT_SALES', 'Satış Raporları'),
('REPORT_STOCK', 'Stok Raporları'),
('REPORT_FINANCE', 'Finansal Raporlar'),

-- Sistem
('SYSTEM_SETTINGS', 'Sistem Ayarlarını Yönetme');

-- ===========================
-- ROL - YETKİ ATAMALARI
-- ===========================

-- ADMIN → tüm yetkiler
INSERT INTO rol_yetki (rol_id, yetki_id)
SELECT r.rol_id, y.yetki_id
FROM rol r
CROSS JOIN yetki y
WHERE r.rol_kodu = 'ADMIN';

-- PHARMACIST → proje kapsamına uygun yetkiler
INSERT INTO rol_yetki (rol_id, yetki_id)
SELECT r.rol_id, y.yetki_id
FROM rol r
JOIN yetki y ON y.yetki_kodu IN (
    'MEDICINE_VIEW', 'MEDICINE_ADD', 'MEDICINE_UPDATE', 'MEDICINE_PRICE_UPDATE', 'MEDICINE_DELETE',
    'STOCK_VIEW', 'STOCK_IN', 'STOCK_OUT', 'LOW_STOCK_VIEW',
    'SALE_CREATE', 'SALE_VIEW', 'SALE_RETURN',
    'REPORT_SALES', 'REPORT_STOCK'
)
WHERE r.rol_kodu = 'PHARMACIST';

-- PERSONNEL → sadece temel işlemler
INSERT INTO rol_yetki (rol_id, yetki_id)
SELECT r.rol_id, y.yetki_id
FROM rol r
JOIN yetki y ON y.yetki_kodu IN (
    'MEDICINE_VIEW', 'STOCK_VIEW',
    'SALE_CREATE', 'SALE_VIEW',
    'CUSTOMER_VIEW' 
)
WHERE r.rol_kodu = 'PERSONNEL';

-- ======================================================
-- DEMO KULLANICILAR
-- ======================================================
INSERT INTO kullanici (kullanici_adi, sifre, rol_id, ad, soyad)
VALUES
('admin', 'admin123', (SELECT rol_id FROM rol WHERE rol_kodu='ADMIN'), 'Sistem', 'Yöneticisi'),
('eczaci', 'eczaci123', (SELECT rol_id FROM rol WHERE rol_kodu='PHARMACIST'), 'Ahmet', 'Eczacı'),
('personel', 'personel123', (SELECT rol_id FROM rol WHERE rol_kodu='PERSONNEL'), 'Ayşe', 'Personel');

-- ======================================================
-- KATEGORILER
-- ======================================================
INSERT INTO kategori (kategori_adi)
VALUES
('Ağrı Kesici'),
('Antibiyotik'),
('Vitamin');

-- ======================================================
-- TEDARIKCILER
-- ======================================================
INSERT INTO tedarikci (firma_adi, yetkili_kisi, telefon, email)
VALUES
('ABC İlaç A.Ş.', 'Mehmet Yılmaz', '05551234567', 'info@abc.com'),
('Sağlık Medikal', 'Ayşe Demir', '05559876543', 'iletisim@saglik.com');

-- ======================================================
-- ILACLAR
-- ======================================================
INSERT INTO ilac (
    ad, barkod, kategori_id, tedarikci_id,
    fiyat, stok, min_stok, receteli_mi
)
VALUES
(
    'Parol',
    '869000000001',
    (SELECT kategori_id FROM kategori WHERE kategori_adi='Ağrı Kesici'),
    (SELECT tedarikci_id FROM tedarikci WHERE firma_adi='ABC İlaç A.Ş.'),
    45.00, 50, 10, FALSE
),
(
    'Augmentin',
    '869000000002',
    (SELECT kategori_id FROM kategori WHERE kategori_adi='Antibiyotik'),
    (SELECT tedarikci_id FROM tedarikci WHERE firma_adi='Sağlık Medikal'),
    120.00, 15, 5, TRUE
);

-- ======================================================
-- MUSTERILER
-- ======================================================
INSERT INTO musteri (ad, soyad, telefon, tc_no)
VALUES
('Ali', 'Kaya', '05550001122', '12345678901'),
('Zeynep', 'Yıldız', '05553334455', '10987654321');