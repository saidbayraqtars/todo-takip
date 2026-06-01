// Bu dosya: Gorev is kurallarinin arayuzu (listeleme, ekleme, guncelleme, silme, dashboard).
// Her metot kullaniciId alir -> veri izolasyonu zorunlu kilinir.
using GorevTakip.Business.DTOs;
using GorevTakip.Entities;
using GorevTakip.Entities.Enums;

namespace GorevTakip.Business.Services;

/// <summary>
/// Görev iş kuralları. Tüm metotlar kullaniciId alır -> kullanıcı veri izolasyonu zorunlu.
/// </summary>
public interface IGorevService
{
    Task<List<Gorev>> ListeleAsync(int kullaniciId);
    Task<List<Gorev>> DurumaGoreAsync(int kullaniciId, GorevDurum durum);
    Task<Gorev?> DetayAsync(int gorevId, int kullaniciId);
    Task<int> EkleAsync(int kullaniciId, string baslik, string? aciklama, DateTime sonTarih);
    Task<bool> GuncelleAsync(int gorevId, int kullaniciId, string baslik, string? aciklama, DateTime sonTarih, GorevDurum durum);
    Task<bool> SilAsync(int gorevId, int kullaniciId);
    Task<DashboardOzet> DashboardAsync(int kullaniciId);
}
