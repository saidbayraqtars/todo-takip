// Bu dosya: IGorevService govdesi. Tum CRUD ve dashboard hesaplari burada.
// Guncelle/Sil islemleri once sahiplik kontrolu yapar (baskasinin gorevine dokunmaz).
using GorevTakip.Business.DTOs;
using GorevTakip.DataAccess.Repositories;
using GorevTakip.Entities;
using GorevTakip.Entities.Enums;

namespace GorevTakip.Business.Services;

public class GorevService : IGorevService
{
    private readonly IGorevRepository _gorevRepo;

    public GorevService(IGorevRepository gorevRepo) => _gorevRepo = gorevRepo;

    public Task<List<Gorev>> ListeleAsync(int kullaniciId) =>
        _gorevRepo.GetByKullaniciAsync(kullaniciId);

    public Task<List<Gorev>> DurumaGoreAsync(int kullaniciId, GorevDurum durum) =>
        _gorevRepo.GetByKullaniciAndDurumAsync(kullaniciId, durum);

    public Task<Gorev?> DetayAsync(int gorevId, int kullaniciId) =>
        _gorevRepo.GetOwnedAsync(gorevId, kullaniciId);

    public async Task<int> EkleAsync(int kullaniciId, string baslik, string? aciklama, DateTime sonTarih)
    {
        var gorev = new Gorev
        {
            KullaniciId = kullaniciId,
            Baslik = baslik.Trim(),
            Aciklama = aciklama?.Trim(),
            SonTarih = sonTarih,
            Durum = GorevDurum.Bekliyor
        };
        await _gorevRepo.AddAsync(gorev);
        await _gorevRepo.SaveChangesAsync();
        return gorev.Id;
    }

    public async Task<bool> GuncelleAsync(int gorevId, int kullaniciId, string baslik, string? aciklama, DateTime sonTarih, GorevDurum durum)
    {
        // Sahiplik kontrolü: başka kullanıcının görevi null döner -> güncelleme yapılmaz.
        var gorev = await _gorevRepo.GetOwnedAsync(gorevId, kullaniciId);
        if (gorev is null) return false;

        gorev.Baslik = baslik.Trim();
        gorev.Aciklama = aciklama?.Trim();
        gorev.SonTarih = sonTarih;
        gorev.Durum = durum;

        _gorevRepo.Update(gorev);
        await _gorevRepo.SaveChangesAsync();
        return true;
    }

    public async Task<bool> SilAsync(int gorevId, int kullaniciId)
    {
        var gorev = await _gorevRepo.GetOwnedAsync(gorevId, kullaniciId);
        if (gorev is null) return false;

        _gorevRepo.Remove(gorev);
        await _gorevRepo.SaveChangesAsync();
        return true;
    }

    public async Task<DashboardOzet> DashboardAsync(int kullaniciId) => new()
    {
        Toplam = await _gorevRepo.CountByKullaniciAsync(kullaniciId),
        Tamamlanan = await _gorevRepo.CountByDurumAsync(kullaniciId, GorevDurum.Tamamlandi),
        Bekleyen = await _gorevRepo.CountByDurumAsync(kullaniciId, GorevDurum.Bekliyor)
    };
}
