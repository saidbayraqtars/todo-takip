// Bu dosya: Uyelik ekranlarinin controller'i.
// Login (giris + session ac), Register (kayit), Profil (kullanici bilgisi), Logout (session kapat).
using GorevTakip.Business.Services;
using GorevTakip.Web.Infrastructure;
using GorevTakip.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace GorevTakip.Web.Controllers;

public class AccountController : Controller
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService) => _authService = authService;

    // GET: /Account/Login  (Ekran 1: Giriş Yap)
    [HttpGet]
    public IActionResult Login()
    {
        if (HttpContext.Session.GetKullaniciId() is not null)
            return RedirectToAction("Index", "Dashboard");

        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var result = await _authService.LoginAsync(model.KullaniciAdi, model.Sifre);
        if (!result.Basarili)
        {
            ModelState.AddModelError(string.Empty, result.Hata!);
            return View(model);
        }

        HttpContext.Session.SetKullanici(result.KullaniciId, result.KullaniciAdi);
        return RedirectToAction("Index", "Dashboard");
    }

    // GET: /Account/Register  (Ekran 2: Kayıt Ol)
    [HttpGet]
    public IActionResult Register()
    {
        if (HttpContext.Session.GetKullaniciId() is not null)
            return RedirectToAction("Index", "Dashboard");

        return View(new RegisterViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var result = await _authService.RegisterAsync(model.KullaniciAdi, model.Sifre, model.AdSoyad);
        if (!result.Basarili)
        {
            ModelState.AddModelError(string.Empty, result.Hata!);
            return View(model);
        }

        // Kayıt sonrası otomatik giriş
        HttpContext.Session.SetKullanici(result.KullaniciId, result.KullaniciAdi);
        return RedirectToAction("Index", "Dashboard");
    }

    // GET: /Account/Profil  (Ekran 10: Profil)
    [HttpGet]
    [SessionAuthorize]
    public async Task<IActionResult> Profil()
    {
        var id = HttpContext.Session.GetKullaniciId()!.Value;
        var kullanici = await _authService.GetByIdAsync(id);
        if (kullanici is null)
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
        return View(kullanici);
    }

    // POST: /Account/Logout  (Çıkış: session kapat)
    [HttpPost]
    [ValidateAntiForgeryToken]
    [SessionAuthorize]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Login));
    }
}
