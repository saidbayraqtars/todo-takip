// Bu dosya: Sifre hash'leme islemlerinin arayuzu.
// Hash (sifreyi gizle) ve Verify (girilen sifre dogru mu) metotlarini tanimlar.
namespace GorevTakip.Business.Security;

public interface IPasswordHasher
{
    /// <summary>Şifreyi salt'lı PBKDF2 hash'e çevirir. Çıktı "salt.hash" base64 formatında.</summary>
    string Hash(string password);

    /// <summary>Düz şifreyi saklanan hash ile karşılaştırır.</summary>
    bool Verify(string password, string storedHash);
}
