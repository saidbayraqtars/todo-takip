// Bu dosya: Kayit Ol formunun model'i. Sifre tekrari ve uzunluk dogrulamalarini icerir.
using System.ComponentModel.DataAnnotations;

namespace GorevTakip.Web.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Kullanıcı adı 3-50 karakter olmalıdır.")]
    [Display(Name = "Kullanıcı Adı")]
    public string KullaniciAdi { get; set; } = string.Empty;

    [StringLength(100)]
    [Display(Name = "Ad Soyad")]
    public string? AdSoyad { get; set; }

    [Required(ErrorMessage = "Şifre zorunludur.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string Sifre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
    [Compare(nameof(Sifre), ErrorMessage = "Şifreler eşleşmiyor.")]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre (Tekrar)")]
    public string SifreTekrar { get; set; } = string.Empty;
}
