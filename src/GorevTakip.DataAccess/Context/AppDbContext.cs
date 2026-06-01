// Bu dosya: Entity Framework Core veritabani baglanti sinifi (DbContext).
// Code First yaklasimi: tablolar buradaki DbSet'lerden ve OnModelCreating
// ayarlarindan uretilir. Kullanici-Gorev arasi 1-* iliski burada tanimlanir.
using GorevTakip.Entities;
using GorevTakip.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace GorevTakip.DataAccess.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Kullanici> Kullanicilar => Set<Kullanici>();
    public DbSet<Gorev> Gorevler => Set<Gorev>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Kullanici
        modelBuilder.Entity<Kullanici>(e =>
        {
            e.HasKey(k => k.Id);
            e.Property(k => k.KullaniciAdi).IsRequired().HasMaxLength(50);
            e.HasIndex(k => k.KullaniciAdi).IsUnique();            // benzersiz kullanıcı adı
            e.Property(k => k.SifreHash).IsRequired();
            e.Property(k => k.AdSoyad).HasMaxLength(100);
        });

        // Gorev
        modelBuilder.Entity<Gorev>(e =>
        {
            e.HasKey(g => g.Id);
            e.Property(g => g.Baslik).IsRequired().HasMaxLength(150);
            e.Property(g => g.Aciklama).HasMaxLength(1000);
            e.Property(g => g.Durum).HasConversion<int>();        // enum -> int

            // one-to-many: Kullanici 1 --- * Gorev
            e.HasOne(g => g.Kullanici)
             .WithMany(k => k.Gorevler)
             .HasForeignKey(g => g.KullaniciId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasIndex(g => g.KullaniciId);
        });

        base.OnModelCreating(modelBuilder);
    }
}
