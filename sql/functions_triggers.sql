-- ===========================
-- ILAC FIYAT DEGISIMI KAYDI FONKSIYONU
-- ===========================
CREATE OR REPLACE FUNCTION ilac_fiyat_log()
RETURNS TRIGGER AS $$
DECLARE
    v_user INT;
BEGIN
    v_user := current_setting('app.current_user', true)::INT;

    IF v_user IS NULL OR v_user = 0 THEN
        RETURN NEW;
    END IF;

    IF OLD.fiyat IS DISTINCT FROM NEW.fiyat THEN
        INSERT INTO ilac_fiyat_gecmis (
            ilac_id,
            eski_fiyat,
            yeni_fiyat,
            degistiren_kullanici
        )
        VALUES (
            OLD.ilac_id,
            OLD.fiyat,
            NEW.fiyat,
            v_user
        );
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;


-- ===========================
-- SATIS DETAY → STOK DUSME FONKSIYONU
-- ===========================
CREATE OR REPLACE FUNCTION satis_detay_stok_dus()
RETURNS TRIGGER AS $$
BEGIN
    -- Stok guncelleme
    UPDATE ilac
    SET stok = stok - NEW.adet
    WHERE ilac_id = NEW.ilac_id;

    -- Stok hareket kaydi
    INSERT INTO stok_hareket (
        ilac_id,
        miktar,
        hareket_tipi,
        aciklama
    )
    VALUES (
        NEW.ilac_id,
        -NEW.adet,
        'CIKIS',
        'Satış #' || NEW.satis_id
    );

    -- Düşük stok kontrol tetikleyebiliriz (trigger ile)
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;


-- ===========================
-- DUSUK STOK UYARI FONKSIYONU
-- ===========================
CREATE OR REPLACE FUNCTION low_stock_check()
RETURNS TRIGGER AS $$
BEGIN
    IF NEW.stok <= NEW.min_stok
       AND NOT EXISTS (
           SELECT 1 FROM low_stock_alert
           WHERE ilac_id = NEW.ilac_id AND okundu = FALSE
       )
    THEN
        INSERT INTO low_stock_alert (ilac_id, stok)
        VALUES (NEW.ilac_id, NEW.stok);
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;


-- ===========================
-- TRIGGER TANIMLARI
-- ===========================

-- ILAC FIYAT DEGISIMI
CREATE TRIGGER trg_ilac_fiyat
AFTER UPDATE OF fiyat ON ilac
FOR EACH ROW
EXECUTE FUNCTION ilac_fiyat_log();

-- SATIS DETAY → STOK DUS
CREATE TRIGGER trg_satis_detay_stok
AFTER INSERT ON satis_detay
FOR EACH ROW
EXECUTE FUNCTION satis_detay_stok_dus();

-- DUSUK STOK KONTROL
CREATE TRIGGER trg_low_stock
AFTER UPDATE OF stok ON ilac
FOR EACH ROW
EXECUTE FUNCTION low_stock_check();

-- SATIŞ YAPMA PROCEDURE 
CREATE OR REPLACE PROCEDURE sp_satis_yap(
    p_musteri_id INT,
    p_kullanici_id INT,
    p_ilac_id INT,
    p_adet INT
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_birim_fiyat NUMERIC(10,2);
    v_toplam_fiyat NUMERIC(12,2);
    v_satis_id INT;
BEGIN
    IF p_adet <= 0 THEN
        RAISE EXCEPTION 'Adet 0’dan büyük olmalıdır';
    END IF;

    SELECT fiyat
    INTO v_birim_fiyat
    FROM ilac
    WHERE ilac_id = p_ilac_id
      AND aktif = TRUE;

    IF v_birim_fiyat IS NULL THEN
        RAISE EXCEPTION 'İlaç bulunamadı veya pasif';
    END IF;

    v_toplam_fiyat := v_birim_fiyat * p_adet;

    INSERT INTO satis (musteri_id, kullanici_id, toplam_fiyat)
    VALUES (p_musteri_id, p_kullanici_id, v_toplam_fiyat)
    RETURNING satis_id INTO v_satis_id;

	INSERT INTO satis_detay (satis_id, ilac_id, adet, birim_fiyat)
    VALUES (v_satis_id, p_ilac_id, p_adet, v_birim_fiyat);

END;
$$;


-- ===========================
-- STOK GIRISI PROCEDURE
-- ===========================

CREATE OR REPLACE PROCEDURE sp_stok_giris(
    p_ilac_id INT,
    p_miktar INT,
    p_aciklama TEXT DEFAULT 'Manuel stok girişi'
)
LANGUAGE plpgsql
AS $$
BEGIN
    BEGIN
        IF p_miktar <= 0 THEN
            RAISE EXCEPTION 'Stok miktarı sıfırdan büyük olmalıdır';
        END IF;

        -- Stok artır
        UPDATE ilac
        SET stok = stok + p_miktar
        WHERE ilac_id = p_ilac_id;

        -- Stok hareket kaydı
        INSERT INTO stok_hareket (
            ilac_id,
            miktar,
            hareket_tipi,
            aciklama
        )
        VALUES (
            p_ilac_id,
            p_miktar,
            'GIRIS',
            p_aciklama
        );

        -- NOT:
        -- Düşük stok kontrolü trigger ile yapılır

        COMMIT;
    EXCEPTION
        WHEN OTHERS THEN
            ROLLBACK;
            RAISE;
    END;
END;
$$;