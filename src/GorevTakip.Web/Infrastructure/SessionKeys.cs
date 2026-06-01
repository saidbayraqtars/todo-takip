// Bu dosya: Session'da tutulan anahtar isimleri + okuma/yazma kisayollari.
// Boylece controller'larda "KullaniciId" yazi yazi tekrar edilmez, tek yerden yonetilir.
using Microsoft.AspNetCore.Http;

namespace GorevTakip.Web.Infrastructure;

public static class SessionKeys
{
    public const string KullaniciId = "KullaniciId";
    public const string KullaniciAdi = "KullaniciAdi";
}

/// <summary>Session okuma kısayolları. Controller'larda tekrar eden kodu önler.</summary>
public static class SessionExtensions
{
    public static int? GetKullaniciId(this ISession session)
    {
        var id = session.GetInt32(SessionKeys.KullaniciId);
        return id;
    }

    public static void SetKullanici(this ISession session, int id, string kullaniciAdi)
    {
        session.SetInt32(SessionKeys.KullaniciId, id);
        session.SetString(SessionKeys.KullaniciAdi, kullaniciAdi);
    }
}
