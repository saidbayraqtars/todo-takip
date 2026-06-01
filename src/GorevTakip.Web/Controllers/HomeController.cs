// HomeController: Sitenin kök adresine (/) gelen istekleri karşılar.
// Kullanıcı giriş yaptıysa Dashboard'a, yapmadıysa Login ekranına yönlendirir.
// Ayrıca hata sayfasını (Error) gösterir.
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GorevTakip.Web.Infrastructure;
using GorevTakip.Web.Models;

namespace GorevTakip.Web.Controllers;

public class HomeController : Controller
{
    // Ana giriş noktası: session durumuna göre yönlendir
    public IActionResult Index()
    {
        if (HttpContext.Session.GetKullaniciId() is not null)
            return RedirectToAction("Index", "Dashboard");

        return RedirectToAction("Login", "Account");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
