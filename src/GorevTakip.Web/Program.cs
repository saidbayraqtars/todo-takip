// Bu dosya: Uygulamanin baslangic noktasi. Servisleri (MVC, katmanlar, Session) kaydeder,
// uygulama acilirken veritabanini migration ile olusturur ve istek hattini (pipeline) kurar.
using GorevTakip.Business;
using GorevTakip.DataAccess;
using GorevTakip.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default")
                       ?? "Data Source=gorevtakip.db";

builder.Services.AddControllersWithViews();

// Katmanları kaydet (Layered DI)
builder.Services.AddDataAccess(connectionString);
builder.Services.AddBusiness();

// Session yönetimi
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;            // XSS ile cookie okunamaz
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// DB'yi migration ile oluştur/güncelle
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();          // Authorization'dan önce: filtreler session'a erişebilsin
app.UseAuthorization();

app.MapStaticAssets();

// Giriş yapılmamışsa Account/Login açılır.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
