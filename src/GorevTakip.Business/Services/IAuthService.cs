// Bu dosya: Uyelik islemlerinin (kayit, giris, kullanici getir) arayuzu.
using GorevTakip.Business.DTOs;
using GorevTakip.Entities;

namespace GorevTakip.Business.Services;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(string kullaniciAdi, string sifre, string? adSoyad);
    Task<AuthResult> LoginAsync(string kullaniciAdi, string sifre);
    Task<Kullanici?> GetByIdAsync(int id);
}
