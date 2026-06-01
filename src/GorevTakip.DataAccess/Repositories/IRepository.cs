// Bu dosya: Tum tablolar icin ortak CRUD metotlarini tanimlayan generic arayuz.
// Amac: her repository'de ayni Add/Get/Update/Delete kodunu tekrar yazmamak.
using System.Linq.Expressions;

namespace GorevTakip.DataAccess.Repositories;

/// <summary>Generic CRUD sözleşmesi. Kod tekrarını önler.</summary>
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task<int> SaveChangesAsync();
}
