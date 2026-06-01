// Bu dosya: Gorev Ekle ve Gorev Guncelle formlarinin ORTAK model'i.
// DuzenlemeModu bayragi ile ayni form iki ekrani da idare eder.
using System.ComponentModel.DataAnnotations;
using GorevTakip.Entities.Enums;

namespace GorevTakip.Web.Models;

/// <summary>Görev Ekle ve Güncelle formları için ortak view model.</summary>
public class GorevFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Başlık zorunludur.")]
    [StringLength(150, ErrorMessage = "Başlık en fazla 150 karakter olabilir.")]
    [Display(Name = "Başlık")]
    public string Baslik { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Açıklama en fazla 1000 karakter olabilir.")]
    [Display(Name = "Açıklama")]
    public string? Aciklama { get; set; }

    [Required(ErrorMessage = "Son tarih zorunludur.")]
    [DataType(DataType.Date)]
    [Display(Name = "Son Tarih")]
    public DateTime SonTarih { get; set; } = DateTime.Today;

    [Display(Name = "Durum")]
    public GorevDurum Durum { get; set; } = GorevDurum.Bekliyor;

    /// <summary>true: Güncelle ekranı, false: Ekle ekranı. View durum alanını buna göre gösterir.</summary>
    public bool DuzenlemeModu { get; set; }
}
