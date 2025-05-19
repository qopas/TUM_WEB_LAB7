using System.Linq.Expressions;
using BookRental.Domain.Interfaces.Repositories;
using BookRental.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookRental.Infrastructure.Repositories;

public class Repository<T>(BookRentalDbContext dbContext) : IRepository<T>
    where T : class
{
    protected readonly BookRentalDbContext _dbContext = dbContext;

    public async Task<T> GetByIdAsync(string id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }
        
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }
        
    public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().Where(predicate).ToListAsync();
    }
        
    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        return entity;
    }
        
    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }
        
    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }
        
    public async Task DeleteAsync(string id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            await DeleteAsync(entity);
        }
    }
}