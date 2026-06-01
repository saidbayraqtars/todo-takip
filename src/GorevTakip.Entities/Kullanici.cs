// Bu dosya: Kullanici tablosunun varlık (entity) sınıfı.
// Bir kullanicinin birden cok gorevi olabilir (one-to-many iliski).
// Sifre acik metin DEGIL, hash olarak SifreHash alaninda tutulur.
using System.ComponentModel.DataAnnotations;

namespace GorevTakip.Entities;

/// <summary>
/// Sistemdeki kullanıcı. Bir kullanıcının birden çok görevi olabilir (one-to-many).
/// </summary>
public class Kullanici
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string KullaniciAdi { get; set; } = string.Empty;

    /// <summary>Şifre asla açık metin tutulmaz; PBKDF2 hash + salt birlikte saklanır.</summary>
    [Required]
    public string SifreHash { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? AdSoyad { get; set; }

    public DateTime OlusturmaTarihi { get; set; } = DateTime.UtcNow;

    // Navigation: bir kullanıcının görevleri
    public ICollection<Gorev> Gorevler { get; set; } = new List<Gorev>();
}
