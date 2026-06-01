# 📋 Görev Takip Sistemi (To-Do)

ASP.NET Core MVC + Entity Framework Core (Code First) ile yazılmış, kullanıcı girişli bir görev takip uygulaması.

Bu README'yi **hiç bilmeyen biri bile** adım adım takip edip projeyi çalıştırabilsin diye yazdım. Sırayla oku, atlama. 🙂

---

## 1. Bu proje ne yapıyor?

- Kayıt ol / giriş yap (her kullanıcının kendi hesabı var)
- Kendi görevlerini ekle, listele, güncelle, sil
- Görevleri "Bekliyor" / "Tamamlandı" diye işaretle
- Dashboard'da toplam / bekleyen / tamamlanan sayılarını gör
- **Herkes sadece kendi görevlerini görür** (başkasının görevine erişemezsin)

---

## 2. Bilgisayarında ne olmalı? (Gereksinimler)

| Gerekli | Sürüm | Kontrol komutu |
|---------|-------|----------------|
| .NET SDK | 10.0+ | `dotnet --version` |

Komutu yaz, bir sürüm numarası çıkıyorsa (örn. `10.0.203`) hazırsın.
Çıkmıyorsa: https://dotnet.microsoft.com/download adresinden ".NET SDK" indir, kur, bilgisayarı yeniden başlat.

> Veritabanı için **ekstra bir şey kurmana gerek YOK**. SQLite kullanıyoruz; tek bir dosyada (`gorevtakip.db`) tutuluyor, uygulama açılınca kendisi oluşturuyor.

---

## 3. Projeyi çalıştırma (en kısa yol)

Terminali (PowerShell) aç, proje klasörüne gel ve şunu yaz:

```powershell
dotnet run --project src/GorevTakip.Web
```

Terminalde şuna benzer bir satır göreceksin:

```
Now listening on: http://localhost:5xxx
```

O adresi tarayıcıda aç. İlk açılışta seni **Giriş** ekranına atar.

Durdurmak için terminalde **Ctrl + C**.

> İlk çalıştırmada uygulama veritabanını otomatik kurar (migration uygular). Ekstra komut gerekmez.

---

## 4. İlk kullanım (adım adım)

1. Tarayıcı açıldı → **Kayıt Ol**'a tıkla.
2. Kullanıcı adı (en az 3 karakter), istersen ad-soyad, şifre (en az 6 karakter) gir. Şifreyi iki kez yaz.
3. **Kayıt Ol** de → otomatik giriş yapıp **Ana Sayfa (Dashboard)**'a düşersin.
4. Üstteki menüden **+ Yeni Görev** → başlık + açıklama + son tarih gir → **Kaydet**.
5. **Görevlerim**'de görev listende görünür. Satırdaki butonlar:
   - **✓ / ↩** : tamamlandı yap / geri al
   - **Düzenle** : görevi güncelle
   - **Sil** : görevi sil (onay ister)
6. Başlığa tıklarsan **Detay** ekranı açılır.
7. Menüden **Bekleyenler** / **Tamamlananlar** ile filtreli listeleri gör.
8. Sağ üstte adına tıkla → **Profil**. Oradan **Çıkış Yap**.

---

## 5. Proje nasıl düzenlenmiş? (Katmanlı Mimari)

Proje 4 ayrı parçaya (katmana) bölünmüş. Her katmanın tek bir işi var. Böylece kod karışmaz:

```
GorevTakip.sln                  → çözüm dosyası (hepsini bir arada tutar)
└─ src/
   ├─ GorevTakip.Entities       → Tablolara karşılık gelen sınıflar (Kullanici, Gorev) + enum
   ├─ GorevTakip.DataAccess     → Veritabanı (DbContext) + sorgular (Repository)
   ├─ GorevTakip.Business       → İş kuralları (giriş/kayıt mantığı, şifre hash, görev kuralları)
   └─ GorevTakip.Web            → Ekranlar: Controller + Razor View + Session
```

**Bağımlılık yönü** (kim kimi tanıyor): `Web → Business → DataAccess → Entities`
Yani üst katman alta bağlı, alt katman üstü bilmez. Temiz mimari budur.

> İpucu: Her kod dosyasının en başında `// Bu dosya: ...` diye bir yorum var. O dosyanın ne işe yaradığını oradan okuyabilirsin.

---

## 6. Veritabanı yapısı

İki tablo var ve aralarında **bire-çok (one-to-many)** ilişki kurulu:

```
Kullanicilar (1) ───────< (çok) Gorevler
   Id (PK)                   Id (PK)
   KullaniciAdi (benzersiz)  Baslik
   SifreHash                 Aciklama
   AdSoyad                   SonTarih
   OlusturmaTarihi           Durum (Bekliyor / Tamamlandi)
                             KullaniciId (FK → Kullanicilar.Id)
```

- **PK** = Primary Key (birincil anahtar), **FK** = Foreign Key (yabancı anahtar).
- Bir kullanıcının çok görevi olur; her görev tek bir kullanıcıya aittir.
- `KullaniciAdi` benzersiz → aynı isimle iki kişi kayıt olamaz.

---

## 7. Güvenlik (önemli noktalar)

- **Şifreler açık metin tutulmaz.** PBKDF2 (SHA-256 + rastgele salt) ile hash'lenir. Veritabanında `salt.hash` görürsün, şifrenin kendisini değil.
- **Session yönetimi.** Giriş yapınca kullanıcı id'si session'a yazılır.
- **Yetkisiz erişim engeli.** Giriş yapmadan korumalı bir sayfaya gitmeye çalışırsan otomatik Giriş ekranına atılırsın.
- **Veri izolasyonu.** Bütün görev sorguları senin kullanıcı id'ne göre filtrelenir; başkasının görevini ne listede görürsün ne de adres çubuğuna id yazarak açabilirsin.

---

## 8. Veritabanını sıfırlamak istersen

Tüm veriyi silip baştan başlamak için: uygulamayı durdur, `gorevtakip.db` dosyasını sil, tekrar `dotnet run` yap. Uygulama boş bir veritabanını yeniden oluşturur.

---

## 9. Sık karşılaşılan sorunlar

| Sorun | Çözüm |
|-------|-------|
| `dotnet` komutu bulunamadı | .NET SDK kurulu değil / PATH'te yok. Kur ve terminali yeniden aç. |
| Port zaten kullanımda | Sabit port: `dotnet run --project src/GorevTakip.Web --urls http://localhost:5080` |
| Tarayıcı "güvenli değil" diyor | `https` yerine `http://localhost:...` adresini kullan (geliştirme ortamı). |
| Migration / tablo hatası | `gorevtakip.db` dosyasını sil, tekrar `dotnet run` yap. |

---

## 10. Faydalı komutlar (ileri seviye)

```powershell
# Sadece derle (çalıştırmadan hata var mı bak)
dotnet build

# Veritabanı modelini değiştirdiysen yeni migration oluştur
dotnet ef migrations add MigrationAdi -p src/GorevTakip.DataAccess -s src/GorevTakip.Web

# Migration'ları veritabanına uygula (uygulama açılışta da otomatik yapar)
dotnet ef database update -p src/GorevTakip.DataAccess -s src/GorevTakip.Web
```

> `dotnet ef` komutu yoksa bir kez şunu kur: `dotnet tool install --global dotnet-ef`

---

## 11. Ekranlar (proje gereksinimi: 10 ekran)

| # | Ekran | Adres |
|---|-------|-------|
| 1 | Giriş Yap | `/Account/Login` |
| 2 | Kayıt Ol | `/Account/Register` |
| 3 | Dashboard (Ana Sayfa) | `/Dashboard` |
| 4 | Görev Listeleme | `/Gorev` |
| 5 | Görev Ekle | `/Gorev/Ekle` |
| 6 | Görev Güncelle | `/Gorev/Guncelle/{id}` |
| 7 | Görev Detay | `/Gorev/Detay/{id}` |
| 8 | Tamamlanan Görevler | `/Gorev/Tamamlananlar` |
| 9 | Bekleyen Görevler | `/Gorev/Bekleyenler` |
| 10 | Profil / Çıkış | `/Account/Profil` |

---

Kolay gelsin! Takıldığın yerde önce yukarıdaki **Sık karşılaşılan sorunlar** tablosuna bak. 🚀
