using BookRental.Domain.Entities.Base;
using BookRental.Domain.Interfaces.Repositories;
using BookRental.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BookRental.Infrastructure.Repositories;

public class FullAuditableRepository<T>(BookRentalDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    : AuditableRepository<T>(dbContext, httpContextAccessor), IFullAuditableRepository<T>
    where T : class, IFullAuditable
{
    public Task SoftDeleteAsync(T entity)
    {
        entity.IsDeleted = true;
        entity.DeletedBy = CurrentUserId;
        entity.DeletedAt = DateTimeOffset.UtcNow;
        
        _dbContext.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }
        
    public async Task<int> SoftDeleteAsync(string id)
    {
        var currentTime = DateTimeOffset.UtcNow;
        
        return await _dbContext.Set<T>()
            .Where(e => EF.Property<string>(e, "Id") == id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(e => e.IsDeleted, true)
                .SetProperty(e => e.DeletedBy, CurrentUserId)
                .SetProperty(e => e.DeletedAt, currentTime));
    }

    public async Task<bool> RestoreAsync(string id)
    {
        var affectedRows = await _dbContext.Set<T>()
            .IgnoreQueryFilters()
            .Where(e => EF.Property<string>(e, "Id") == id && e.IsDeleted)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(e => e.IsDeleted, false)
                .SetProperty(e => e.DeletedBy, (string)null)
                .SetProperty(e => e.DeletedAt, (DateTimeOffset?)null));
        
        return affectedRows > 0;
    }

    public IQueryable<T> GetAllIncludingDeleted()
    {
        return _dbContext.Set<T>().IgnoreQueryFilters().AsNoTracking();
    }

    public IQueryable<T> GetOnlyDeleted()
    {
        return _dbContext.Set<T>().IgnoreQueryFilters().Where(e => e.IsDeleted).AsNoTracking();
    }

    public IQueryable<T> GetByIdIncludingDeleted(string id)
    {
        return _dbContext.Set<T>().IgnoreQueryFilters().Where(e => EF.Property<string>(e, "Id") == id);
    }
}