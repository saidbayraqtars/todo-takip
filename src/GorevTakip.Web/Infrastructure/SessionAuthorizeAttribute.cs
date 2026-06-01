// Bu dosya: Korumali sayfalar icin guvenlik filtresi. Action calismadan once session'a bakar;
// kullanici giris yapmamissa otomatik Login ekranina yonlendirir (yetkisiz erisim engeli).
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GorevTakip.Web.Infrastructure;

/// <summary>
/// Session'da KullaniciId yoksa erişimi engeller ve Login'e yönlendirir.
/// Yetkisiz (login olmayan) kişilerin korumalı sayfalara girmesini durdurur.
/// </summary>
public class SessionAuthorizeAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var kullaniciId = context.HttpContext.Session.GetKullaniciId();
        if (kullaniciId is null)
        {
            context.Result = new RedirectToActionResult("Login", "Account", null);
        }

        base.OnActionExecuting(context);
    }
}
