// Bu dosya: Login/Register islemlerinin sonucunu tasiyan kucuk veri sinifi (DTO).
// Basarili ise kullanici bilgisi, basarisiz ise hata mesaji doner.
namespace GorevTakip.Business.DTOs;

/// <summary>Login/Register sonucunu taşır. Controller buna göre session açar veya hata gösterir.</summary>
public class AuthResult
{
    public bool Basarili { get; private set; }
    public string? Hata { get; private set; }
    public int KullaniciId { get; private set; }
    public string KullaniciAdi { get; private set; } = string.Empty;

    public static AuthResult Ok(int id, string kullaniciAdi) => new()
    {
        Basarili = true,
        KullaniciId = id,
        KullaniciAdi = kullaniciAdi
    };

    public static AuthResult Fail(string hata) => new()
    {
        Basarili = false,
        Hata = hata
    };
}
