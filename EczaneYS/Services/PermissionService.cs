using EczaneYS.Data;
using System;
using System.Collections.Generic;

namespace EczaneYS.Services
{
    public static class PermissionService
    {
        // Basit cache: yetkiKodu -> bool
        private static readonly Dictionary<string, bool> _permissionCache =
            new Dictionary<string, bool>();

        public static bool HasPermission(string yetkiKodu)
        {
            // Güvenlik: login olmadan çağrılırsa
            if (string.IsNullOrWhiteSpace(AuthContext.Role))
                return false;

            // ADMIN her şeye yetkilidir
            if (AuthContext.Role == "ADMIN")
                return true;

            // Cache kontrolü
            if (_permissionCache.ContainsKey(yetkiKodu))
                return _permissionCache[yetkiKodu];

            try
            {
                object result = DBHelper.ExecuteScalar(@"
                    SELECT 1
                    FROM rol_yetki ry
                    JOIN yetki y ON y.yetki_id = ry.yetki_id
                    JOIN rol r ON r.rol_id = ry.rol_id
                    WHERE r.rol_kodu = @rol
                      AND y.yetki_kodu = @yetki
                      AND y.aktif = TRUE
                      AND r.aktif = TRUE
                ",
                "@rol", AuthContext.Role,
                "@yetki", yetkiKodu
                );

                bool hasPermission = result != null;

                // Cache'e yaz
                _permissionCache[yetkiKodu] = hasPermission;

                return hasPermission;
            }
            catch
            {
                // Yetki kontrolü hata verirse UI çökmemeli
                return false;
            }
        }

        // Kullanıcı çıkış yapınca cache temizlenebilir
        public static void ClearCache()
        {
            _permissionCache.Clear();
        }
    }
}
