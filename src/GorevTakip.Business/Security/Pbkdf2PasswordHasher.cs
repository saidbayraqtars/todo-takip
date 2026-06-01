// Bu dosya: Sifreyi PBKDF2 (SHA-256) ile hash'ler. Her sifreye rastgele "salt" eklenir.
// Veritabaninda "salt.hash" seklinde saklanir; sifre asla acik metin tutulmaz.
using System.Security.Cryptography;

namespace GorevTakip.Business.Security;

/// <summary>
/// PBKDF2 (SHA-256) tabanlı şifre hash'leme. Her şifre için rastgele 16 byte salt üretilir,
/// salt ve hash "salt.hash" biçiminde base64 olarak birlikte saklanır.
/// Şifreler hiçbir zaman açık metin tutulmaz.
/// </summary>
public class Pbkdf2PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;       // 128-bit
    private const int KeySize = 32;        // 256-bit
    private const int Iterations = 100_000;
    private static readonly HashAlgorithmName Algo = HashAlgorithmName.SHA256;

    public string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algo, KeySize);
        return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    public bool Verify(string password, string storedHash)
    {
        var parts = storedHash.Split('.', 2);
        if (parts.Length != 2) return false;

        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] expected = Convert.FromBase64String(parts[1]);
        byte[] actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algo, expected.Length);

        // Zamanlama saldırılarına karşı sabit zamanlı karşılaştırma.
        return CryptographicOperations.FixedTimeEquals(actual, expected);
    }
}
