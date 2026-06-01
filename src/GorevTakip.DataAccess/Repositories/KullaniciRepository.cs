// Bu dosya: IKullaniciRepository govdesi. Kullanici adina gore arama ve
// "bu kullanici adi alinmis mi" kontrolu burada yapilir.
using GorevTakip.DataAccess.Context;
using GorevTakip.Entities;
using Microsoft.EntityFrameworkCore;

namespace GorevTakip.DataAccess.Repositories;

public class KullaniciRepository : Repository<Kullanici>, IKullaniciRepository
{
    public KullaniciRepository(AppDbContext context) : base(context) { }

    public async Task<Kullanici?> GetByKullaniciAdiAsync(string kullaniciAdi) =>
        await Set.FirstOrDefaultAsync(k => k.KullaniciAdi == kullaniciAdi);

    public async Task<bool> KullaniciAdiVarMiAsync(string kullaniciAdi) =>
        await Set.AnyAsync(k => k.KullaniciAdi == kullaniciAdi);
}
