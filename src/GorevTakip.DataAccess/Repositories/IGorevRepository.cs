// Bu dosya: Gorev tablosuna ozel sorgularin arayuzu.
// Tum sorgular kullaniciId ile sinirli -> bir kullanici baskasinin gorevini goremez.
using GorevTakip.Entities;
using GorevTakip.Entities.Enums;

namespace GorevTakip.DataAccess.Repositories;

public interface IGorevRepository : IRepository<Gorev>
{
    // Tüm sorgular kullaniciId ile sınırlı -> veri izolasyonu DataAccess seviyesinde garanti.
    Task<List<Gorev>> GetByKullaniciAsync(int kullaniciId);
    Task<List<Gorev>> GetByKullaniciAndDurumAsync(int kullaniciId, GorevDurum durum);
    Task<Gorev?> GetOwnedAsync(int gorevId, int kullaniciId);
    Task<int> CountByDurumAsync(int kullaniciId, GorevDurum durum);
    Task<int> CountByKullaniciAsync(int kullaniciId);
}
