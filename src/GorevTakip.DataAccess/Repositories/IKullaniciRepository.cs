// Bu dosya: Kullanici tablosuna ozel sorgularin arayuzu (kullanici adina gore bulma vb.).
using GorevTakip.Entities;

namespace GorevTakip.DataAccess.Repositories;

public interface IKullaniciRepository : IRepository<Kullanici>
{
    Task<Kullanici?> GetByKullaniciAdiAsync(string kullaniciAdi);
    Task<bool> KullaniciAdiVarMiAsync(string kullaniciAdi);
}
