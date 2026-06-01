// Bu dosya: Gorev (To-Do) tablosunun varlik (entity) sinifi.
// Her gorev tek bir kullaniciya aittir -> KullaniciId foreign key ile baglanir.
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GorevTakip.Entities.Enums;

namespace GorevTakip.Entities;

/// <summary>
/// Görev (To-Do) kaydı. Her görev tek bir kullanıcıya aittir (FK: KullaniciId).
/// </summary>
public class Gorev
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Başlık zorunludur.")]
    [MaxLength(150)]
    public string Baslik { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Aciklama { get; set; }

    /// <summary>Görevin son/teslim tarihi.</summary>
    public DateTime SonTarih { get; set; }

    public GorevDurum Durum { get; set; } = GorevDurum.Bekliyor;

    public DateTime OlusturmaTarihi { get; set; } = DateTime.UtcNow;

    // Foreign key -> Kullanici
    public int KullaniciId { get; set; }

    [ForeignKey(nameof(KullaniciId))]
    public Kullanici? Kullanici { get; set; }
}
