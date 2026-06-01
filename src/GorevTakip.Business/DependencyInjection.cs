// Bu dosya: Business katmanindaki servisleri DI'ya kaydeder (AddBusiness ile tek satir).
using GorevTakip.Business.Security;
using GorevTakip.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GorevTakip.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasher, Pbkdf2PasswordHasher>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IGorevService, GorevService>();
        return services;
    }
}
