using System.Linq.Expressions;
using BoardGamesShop.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesShop.DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly IDbContextFactory<BoardGamesShopDbContext> _contextFactory;

    public Repository(IDbContextFactory<BoardGamesShopDbContext> contextFactory) =>
        _contextFactory = contextFactory;
    

    public IEnumerable<T> GetAll()
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Set<T>().AsNoTracking()
            .ToList();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        using var dbContext = await _contextFactory.CreateDbContextAsync();
        return dbContext.Set<T>().AsNoTracking()
            .ToList();
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Set<T>().AsNoTracking()
            .Where(predicate)
            .ToList();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
    {
        using var dbContext = await _contextFactory.CreateDbContextAsync();
        return dbContext.Set<T>().AsNoTracking()
            .Where(predicate)
            .ToList();
    }

    public T? GetById(int id)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Set<T>().AsNoTracking()
            .FirstOrDefault(x => x.Id == id);
    }

    public T? GetById(Guid id)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Set<T>().AsNoTracking()
            .FirstOrDefault(x => x.ExternalId == id);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        using var dbContext = await _contextFactory.CreateDbContextAsync();
        return await dbContext.Set<T>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        using var dbContext = await _contextFactory.CreateDbContextAsync();
        return await dbContext.Set<T>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.ExternalId == id);
    }

    public T Save(T entity)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        if (dbContext.Set<T>().AsNoTracking().Any(x => x.Id == entity.Id))
        {
            entity.ModificationTime = DateTime.UtcNow;
            var result = dbContext.Set<T>().Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
            return result.Entity;
        }
        else
        {
            entity.ExternalId = Guid.NewGuid();
            entity.CreationTime = DateTime.UtcNow;
            entity.ModificationTime = entity.CreationTime;
            var result = dbContext.Set<T>().Add(entity);
            dbContext.SaveChanges();
            return result.Entity;
        }
    }

    public async Task<T> SaveAsync(T entity)
    {
        using var dbContext = await _contextFactory.CreateDbContextAsync();
        if (await dbContext.Set<T>().AsNoTracking().AnyAsync(x => x.Id == entity.Id))
        {
            entity.ModificationTime = DateTime.UtcNow;
            var result = dbContext.Set<T>().Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }
        else
        {
            entity.ExternalId = Guid.NewGuid();
            entity.CreationTime = DateTime.UtcNow;
            entity.ModificationTime = entity.CreationTime;
            var result = await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }
    }

    public void Delete(T entity)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        dbContext.Set<T>().Remove(entity);
        dbContext.SaveChanges();
    }

    public async Task DeleteAsync(T entity)
    {
        using var dbContext = await _contextFactory.CreateDbContextAsync();
        dbContext.Set<T>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}