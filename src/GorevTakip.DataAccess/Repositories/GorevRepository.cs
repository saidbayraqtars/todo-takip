// Bu dosya: IGorevRepository govdesi. Kullaniciya gore listeleme, duruma gore
// filtreleme, sahiplik kontrolu (GetOwned) ve dashboard sayaclari burada.
using GorevTakip.DataAccess.Context;
using GorevTakip.Entities;
using GorevTakip.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace GorevTakip.DataAccess.Repositories;

public class GorevRepository : Repository<Gorev>, IGorevRepository
{
    public GorevRepository(AppDbContext context) : base(context) { }

    public async Task<List<Gorev>> GetByKullaniciAsync(int kullaniciId) =>
        await Set.Where(g => g.KullaniciId == kullaniciId)
                 .OrderByDescending(g => g.OlusturmaTarihi)
                 .ToListAsync();

    public async Task<List<Gorev>> GetByKullaniciAndDurumAsync(int kullaniciId, GorevDurum durum) =>
        await Set.Where(g => g.KullaniciId == kullaniciId && g.Durum == durum)
                 .OrderByDescending(g => g.OlusturmaTarihi)
                 .ToListAsync();

    // Sahiplik kontrolü: id + kullaniciId birlikte -> başkasının görevine erişim engellenir.
    public async Task<Gorev?> GetOwnedAsync(int gorevId, int kullaniciId) =>
        await Set.FirstOrDefaultAsync(g => g.Id == gorevId && g.KullaniciId == kullaniciId);

    public async Task<int> CountByDurumAsync(int kullaniciId, GorevDurum durum) =>
        await Set.CountAsync(g => g.KullaniciId == kullaniciId && g.Durum == durum);

    public async Task<int> CountByKullaniciAsync(int kullaniciId) =>
        await Set.CountAsync(g => g.KullaniciId == kullaniciId);
}
