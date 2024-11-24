using System.Linq.Expressions;
using BoardGamesShop.DataAccess.Entities;

namespace BoardGamesShop.DataAccess.Repository;

public interface IRepository<T> where T : BaseEntity
{
    IEnumerable<T> GetAll();
    Task<IEnumerable<T>> GetAllAsync();
    IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
    T? GetById(int id);
    T?  GetById(Guid id);
    Task<T?> GetByIdAsync(int id);
    Task<T?>  GetByIdAsync(Guid id);
    T  Save(T entity);
    Task<T> SaveAsync(T entity);
    void Delete(T entity);
    Task DeleteAsync(T entity);
}