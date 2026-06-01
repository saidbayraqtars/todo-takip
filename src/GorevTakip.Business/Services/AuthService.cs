// Bu dosya: Uyelik is kurallari. Kayitta kullanici adi benzersiz mi kontrol eder,
// sifreyi hash'leyip kaydeder. Giriste sifreyi dogrular. Acik metin sifre tutmaz.
using GorevTakip.Business.DTOs;
using GorevTakip.Business.Security;
using GorevTakip.DataAccess.Repositories;
using GorevTakip.Entities;

namespace GorevTakip.Business.Services;

public class AuthService : IAuthService
{
    private readonly IKullaniciRepository _kullaniciRepo;
    private readonly IPasswordHasher _hasher;

    public AuthService(IKullaniciRepository kullaniciRepo, IPasswordHasher hasher)
    {
        _kullaniciRepo = kullaniciRepo;
        _hasher = hasher;
    }

    public async Task<AuthResult> RegisterAsync(string kullaniciAdi, string sifre, string? adSoyad)
    {
        kullaniciAdi = kullaniciAdi.Trim();

        if (await _kullaniciRepo.KullaniciAdiVarMiAsync(kullaniciAdi))
            return AuthResult.Fail("Bu kullanıcı adı zaten alınmış.");

        var kullanici = new Kullanici
        {
            KullaniciAdi = kullaniciAdi,
            AdSoyad = adSoyad?.Trim(),
            SifreHash = _hasher.Hash(sifre)
        };

        await _kullaniciRepo.AddAsync(kullanici);
        await _kullaniciRepo.SaveChangesAsync();

        return AuthResult.Ok(kullanici.Id, kullanici.KullaniciAdi);
    }

    public async Task<AuthResult> LoginAsync(string kullaniciAdi, string sifre)
    {
        var kullanici = await _kullaniciRepo.GetByKullaniciAdiAsync(kullaniciAdi.Trim());

        // Aynı hata mesajı -> kullanıcı adının var olup olmadığını sızdırmaz.
        if (kullanici is null || !_hasher.Verify(sifre, kullanici.SifreHash))
            return AuthResult.Fail("Kullanıcı adı veya şifre hatalı.");

        return AuthResult.Ok(kullanici.Id, kullanici.KullaniciAdi);
    }

    public Task<Kullanici?> GetByIdAsync(int id) => _kullaniciRepo.GetByIdAsync(id);
}
