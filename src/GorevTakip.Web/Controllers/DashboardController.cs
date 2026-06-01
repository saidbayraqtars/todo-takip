// Bu dosya: Ana sayfa (Dashboard) controller'i. Giris yapan kullanicinin toplam,
// tamamlanan ve bekleyen gorev sayilarini hesaplayip ekrana gonderir.
using GorevTakip.Business.Services;
using GorevTakip.Web.Infrastructure;
using GorevTakip.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace GorevTakip.Web.Controllers;

[SessionAuthorize]
public class DashboardController : BaseController
{
    private readonly IGorevService _gorevService;

    public DashboardController(IGorevService gorevService) => _gorevService = gorevService;

    // GET: /Dashboard  (Ekran 3: Dashboard / özet sayaçlar)
    public async Task<IActionResult> Index()
    {
        var ozet = await _gorevService.DashboardAsync(AktifKullaniciId);
        var gorevler = await _gorevService.ListeleAsync(AktifKullaniciId);

        var model = new DashboardViewModel
        {
            Toplam = ozet.Toplam,
            Tamamlanan = ozet.Tamamlanan,
            Bekleyen = ozet.Bekleyen,
            KullaniciAdi = HttpContext.Session.GetString(SessionKeys.KullaniciAdi) ?? string.Empty,
            YaklasanGorevler = gorevler
                .Where(g => g.Durum == Entities.Enums.GorevDurum.Bekliyor)
                .OrderBy(g => g.SonTarih)
                .Take(5)
                .ToList()
        };

        return View(model);
    }
}
