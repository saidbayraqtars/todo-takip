// Bu dosya: Korumali controller'lar icin ortak taban sinif.
// Aktif kullanicinin id'sini session'dan tek noktadan verir (kod tekrarini onler).
using GorevTakip.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace GorevTakip.Web.Controllers;

/// <summary>Korumalı controller'lar için ortak taban. Aktif kullanıcı id'sini session'dan verir.</summary>
public abstract class BaseController : Controller
{
    /// <summary>Session'daki aktif kullanıcı id. SessionAuthorize sayesinde burada null olmaz.</summary>
    protected int AktifKullaniciId => HttpContext.Session.GetKullaniciId() ?? 0;
}
