// Bu dosya: IRepository arayuzunun EF Core ile yazilmis generic govdesi.
// Temel CRUD islemleri (ekle/listele/guncelle/sil/kaydet) tek yerde toplanir.
using System.Linq.Expressions;
using GorevTakip.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace GorevTakip.DataAccess.Repositories;

/// <summary>Generic EF Core repository. Tüm temel CRUD burada tek noktada.</summary>
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext Context;
    protected readonly DbSet<T> Set;

    public Repository(AppDbContext context)
    {
        Context = context;
        Set = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id) => await Set.FindAsync(id);

    public async Task<List<T>> GetAllAsync() => await Set.ToListAsync();

    public async Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate) =>
        await Set.Where(predicate).ToListAsync();

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate) =>
        await Set.FirstOrDefaultAsync(predicate);

    public async Task AddAsync(T entity) => await Set.AddAsync(entity);

    public void Update(T entity) => Set.Update(entity);

    public void Remove(T entity) => Set.Remove(entity);

    public async Task<int> SaveChangesAsync() => await Context.SaveChangesAsync();
}
