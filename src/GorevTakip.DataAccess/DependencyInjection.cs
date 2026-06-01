// Bu dosya: DataAccess katmanindaki siniflari (DbContext + repository'ler) DI'ya kaydeder.
// Program.cs tek satirla (AddDataAccess) bu katmani baglar -> temiz kurulum.
using GorevTakip.DataAccess.Context;
using GorevTakip.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GorevTakip.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(connectionString));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IKullaniciRepository, KullaniciRepository>();
        services.AddScoped<IGorevRepository, GorevRepository>();

        return services;
    }
}
