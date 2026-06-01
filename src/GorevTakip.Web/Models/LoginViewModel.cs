// Bu dosya: Giris Yap formunun model'i. Kullanici adi + sifre alanlari ve dogrulama kurallari.
using System.ComponentModel.DataAnnotations;

namespace GorevTakip.Web.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
    [Display(Name = "Kullanıcı Adı")]
    public string KullaniciAdi { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre zorunludur.")]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string Sifre { get; set; } = string.Empty;
}
