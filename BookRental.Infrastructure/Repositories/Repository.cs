using System.Linq.Expressions;
using BookRental.Domain.Interfaces.Repositories;
using BookRental.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace BookRental.Infrastructure.Repositories;

public class Repository<T>(BookRentalDbContext dbContext) : IRepository<T> where T : class
{
    protected readonly BookRentalDbContext _dbContext = dbContext;

    public async Task<T> GetByIdAsync(string id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }
        
    public IQueryable<T> GetAll()
    {
        return _dbContext.Set<T>().AsNoTracking();
    }
        
    public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return _dbContext.Set<T>().Where(predicate);
    }

    public Task HardRemoveAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }
        
    public async Task HardRemoveAsync(string id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            await HardRemoveAsync(entity);
        }
    }
}