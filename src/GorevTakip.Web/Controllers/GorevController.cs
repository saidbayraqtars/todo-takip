// Bu dosya: Gorevlerin tum CRUD ekranlarinin controller'i.
// Listele, Ekle, Guncelle, Detay, Sil + Tamamlanan/Bekleyen filtreleri burada.
// [SessionAuthorize] sayesinde sadece giris yapan kullanici erisebilir.
using GorevTakip.Business.Services;
using GorevTakip.Entities.Enums;
using GorevTakip.Web.Infrastructure;
using GorevTakip.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace GorevTakip.Web.Controllers;

[SessionAuthorize]
public class GorevController : BaseController
{
    private readonly IGorevService _gorevService;

    public GorevController(IGorevService gorevService) => _gorevService = gorevService;

    // GET: /Gorev  (Ekran 4: Görev Listeleme)
    public async Task<IActionResult> Index()
    {
        var gorevler = await _gorevService.ListeleAsync(AktifKullaniciId);
        return View(gorevler);
    }

    // GET: /Gorev/Tamamlananlar  (Ekran 8)
    public async Task<IActionResult> Tamamlananlar()
    {
        var gorevler = await _gorevService.DurumaGoreAsync(AktifKullaniciId, GorevDurum.Tamamlandi);
        ViewData["Baslik"] = "Tamamlanan Görevler";
        return View("Filtre", gorevler);
    }

    // GET: /Gorev/Bekleyenler  (Ekran 9)
    public async Task<IActionResult> Bekleyenler()
    {
        var gorevler = await _gorevService.DurumaGoreAsync(AktifKullaniciId, GorevDurum.Bekliyor);
        ViewData["Baslik"] = "Bekleyen Görevler";
        return View("Filtre", gorevler);
    }

    // GET: /Gorev/Detay/5  (Ekran 7: Görev Detay)
    public async Task<IActionResult> Detay(int id)
    {
        var gorev = await _gorevService.DetayAsync(id, AktifKullaniciId);
        if (gorev is null) return NotFound();
        return View(gorev);
    }

    // GET: /Gorev/Ekle  (Ekran 5: Görev Ekle)
    [HttpGet]
    public IActionResult Ekle() => View(new GorevFormViewModel { SonTarih = DateTime.Today });

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Ekle(GorevFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        await _gorevService.EkleAsync(AktifKullaniciId, model.Baslik, model.Aciklama, model.SonTarih);
        TempData["Mesaj"] = "Görev eklendi.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Gorev/Guncelle/5  (Ekran 6: Görev Güncelle)
    [HttpGet]
    public async Task<IActionResult> Guncelle(int id)
    {
        var gorev = await _gorevService.DetayAsync(id, AktifKullaniciId);
        if (gorev is null) return NotFound();

        var model = new GorevFormViewModel
        {
            Id = gorev.Id,
            Baslik = gorev.Baslik,
            Aciklama = gorev.Aciklama,
            SonTarih = gorev.SonTarih,
            Durum = gorev.Durum,
            DuzenlemeModu = true
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Guncelle(GorevFormViewModel model)
    {
        model.DuzenlemeModu = true;
        if (!ModelState.IsValid) return View(model);

        var ok = await _gorevService.GuncelleAsync(
            model.Id, AktifKullaniciId, model.Baslik, model.Aciklama, model.SonTarih, model.Durum);

        if (!ok) return NotFound();

        TempData["Mesaj"] = "Görev güncellendi.";
        return RedirectToAction(nameof(Detay), new { id = model.Id });
    }

    // POST: /Gorev/Sil/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Sil(int id)
    {
        var ok = await _gorevService.SilAsync(id, AktifKullaniciId);
        if (!ok) return NotFound();

        TempData["Mesaj"] = "Görev silindi.";
        return RedirectToAction(nameof(Index));
    }

    // POST: /Gorev/DurumDegistir/5  (listede hızlı tamamla/geri al)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DurumDegistir(int id)
    {
        var gorev = await _gorevService.DetayAsync(id, AktifKullaniciId);
        if (gorev is null) return NotFound();

        var yeni = gorev.Durum == GorevDurum.Tamamlandi ? GorevDurum.Bekliyor : GorevDurum.Tamamlandi;
        await _gorevService.GuncelleAsync(gorev.Id, AktifKullaniciId, gorev.Baslik, gorev.Aciklama, gorev.SonTarih, yeni);

        return RedirectToAction(nameof(Index));
    }
}
