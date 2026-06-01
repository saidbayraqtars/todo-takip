// Bu dosya: Sadece "dotnet ef migrations" komutlari icin DbContext uretir.
// Uygulama calisirken kullanilmaz; migration araclari tasarim aninda buna ihtiyac duyar.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GorevTakip.DataAccess.Context;

/// <summary>
/// Sadece 'dotnet ef' migration komutları için kullanılır. Runtime DI ile alakası yoktur.
/// DB dosyası Web projesinin çalışma dizininde (gorevtakip.db) oluşur.
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("Data Source=gorevtakip.db")
            .Options;
        return new AppDbContext(options);
    }
}
