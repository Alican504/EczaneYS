-- ===========================
-- EXTENSIONS
-- ===========================
CREATE EXTENSION IF NOT EXISTS pgcrypto;

-- ===========================
-- ROLE TABLE
-- ===========================
CREATE TABLE rol (
    rol_id SERIAL PRIMARY KEY,
    rol_kodu VARCHAR(50) UNIQUE NOT NULL,
    rol_adi VARCHAR(50) NOT NULL,
    aktif BOOLEAN DEFAULT TRUE
);

-- ===========================
-- PERMISSION TABLE
-- ===========================
CREATE TABLE yetki (
    yetki_id SERIAL PRIMARY KEY,
    yetki_kodu VARCHAR(50) UNIQUE NOT NULL,
    yetki_adi VARCHAR(100) NOT NULL,
    aktif BOOLEAN DEFAULT TRUE
);

-- ===========================
-- ROLE - PERMISSION RELATION
-- ===========================
CREATE TABLE rol_yetki (
    rol_id INT REFERENCES rol(rol_id) ON DELETE CASCADE,
    yetki_id INT REFERENCES yetki(yetki_id) ON DELETE CASCADE,
    PRIMARY KEY (rol_id, yetki_id)
);

-- ===========================
-- USER TABLE
-- ===========================
CREATE TABLE kullanici (
    kullanici_id SERIAL PRIMARY KEY,
    kullanici_adi VARCHAR(50) UNIQUE NOT NULL,
    sifre TEXT NOT NULL, -- HASHLENMIS
    rol_id INT NOT NULL REFERENCES rol(rol_id),
    ad VARCHAR(100),
    soyad VARCHAR(100),
    aktif BOOLEAN DEFAULT TRUE,
    olusturma_tarih TIMESTAMP DEFAULT NOW(),
    son_giris TIMESTAMP
);

-- ===========================
-- SUPPLIER TABLE
-- ===========================
CREATE TABLE tedarikci (
    tedarikci_id SERIAL PRIMARY KEY,
    firma_adi VARCHAR(150) UNIQUE NOT NULL,
    yetkili_kisi VARCHAR(100),
    telefon VARCHAR(20),
    adres TEXT,
    email VARCHAR(100) UNIQUE,
    aktif BOOLEAN DEFAULT TRUE
);

-- ===========================
-- CATEGORY TABLE
-- ===========================
CREATE TABLE kategori (
    kategori_id SERIAL PRIMARY KEY,
    kategori_adi VARCHAR(100) UNIQUE NOT NULL,
    aktif BOOLEAN DEFAULT TRUE
);

-- ===========================
-- MEDICINE TABLE
-- ===========================
CREATE TABLE ilac (
    ilac_id SERIAL PRIMARY KEY,
    ad VARCHAR(150) NOT NULL,
    barkod VARCHAR(50) UNIQUE,
    kategori_id INT REFERENCES kategori(kategori_id) ON DELETE SET NULL,
    tedarikci_id INT REFERENCES tedarikci(tedarikci_id),
    fiyat NUMERIC(10,2) NOT NULL CHECK (fiyat >= 0),
    stok INT NOT NULL DEFAULT 0 CHECK (stok >= 0),
    min_stok INT DEFAULT 10,
    receteli_mi BOOLEAN DEFAULT FALSE,
    son_kullanma_tarihi DATE,
    aktif BOOLEAN DEFAULT TRUE
);

-- ===========================
-- MEDICINE PRICE HISTORY
-- ===========================
CREATE TABLE ilac_fiyat_gecmis (
    fiyat_id SERIAL PRIMARY KEY,
    ilac_id INT REFERENCES ilac(ilac_id) ON DELETE CASCADE,
    eski_fiyat NUMERIC(10,2),
    yeni_fiyat NUMERIC(10,2),
    degistiren_kullanici INT REFERENCES kullanici(kullanici_id),
    tarih TIMESTAMP DEFAULT NOW()
);

-- ===========================
-- CUSTOMER TABLE
-- ===========================
CREATE TABLE musteri (
    musteri_id SERIAL PRIMARY KEY,
    ad VARCHAR(100) NOT NULL,
    soyad VARCHAR(100),
    telefon VARCHAR(20),
    tc_no VARCHAR(11) UNIQUE CHECK (char_length(tc_no) = 11),
    aktif BOOLEAN DEFAULT TRUE
);

-- ===========================
-- SALES TABLE
-- ===========================
CREATE TABLE satis (
    satis_id SERIAL PRIMARY KEY,
    musteri_id INT REFERENCES musteri(musteri_id) ON DELETE SET NULL,
    kullanici_id INT REFERENCES kullanici(kullanici_id) ON DELETE SET NULL,
    toplam_fiyat NUMERIC(12,2) NOT NULL,
    tarih TIMESTAMP DEFAULT NOW()
);

ALTER TABLE satis
ADD COLUMN is_returned BOOLEAN NOT NULL DEFAULT FALSE;


-- ===========================
-- SALES DETAIL TABLE
-- ===========================
CREATE TABLE satis_detay (
    satis_detay_id SERIAL PRIMARY KEY,
    satis_id INT REFERENCES satis(satis_id) ON DELETE CASCADE,
    ilac_id INT REFERENCES ilac(ilac_id),
    adet INT NOT NULL CHECK (adet > 0),
    birim_fiyat NUMERIC(10,2) NOT NULL
);

-- ===========================
-- STOCK MOVEMENT
-- ===========================
CREATE TYPE stok_hareket_tipi AS ENUM ('GIRIS', 'CIKIS', 'IADE');

CREATE TABLE stok_hareket (
    hareket_id SERIAL PRIMARY KEY,
    ilac_id INT REFERENCES ilac(ilac_id) ON DELETE CASCADE,
    miktar INT NOT NULL,
    hareket_tipi stok_hareket_tipi NOT NULL,
    aciklama TEXT,
    tarih TIMESTAMP DEFAULT NOW()
);

-- ===========================
-- LOW STOCK ALERT
-- ===========================
CREATE TABLE low_stock_alert (
    alert_id SERIAL PRIMARY KEY,
    ilac_id INT REFERENCES ilac(ilac_id) ON DELETE CASCADE,
    stok INT NOT NULL,
    olusturma_tarih TIMESTAMP DEFAULT NOW(),
    okundu BOOLEAN DEFAULT FALSE
);

-- ======================================================
-- STOK NEGATIF OLAMAZ (VERI BUTUNLUGU)
-- ======================================================
ALTER TABLE ilac
ADD CONSTRAINT chk_stok_negatif_olamaz
CHECK (stok >= 0);

-- ===========================
-- INDEXLER
-- ===========================
CREATE INDEX idx_kullanici_rol ON kullanici(rol_id);
CREATE INDEX idx_satis_musteri ON satis(musteri_id);
CREATE INDEX idx_satis_kullanici ON satis(kullanici_id);
CREATE INDEX idx_satis_detay_satis ON satis_detay(satis_id);
CREATE INDEX idx_stok_hareket_ilac ON stok_hareket(ilac_id);
CREATE INDEX idx_low_stock_ilac ON low_stock_alert(ilac_id);