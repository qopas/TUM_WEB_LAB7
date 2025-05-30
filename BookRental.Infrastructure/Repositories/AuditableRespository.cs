using System.Linq.Expressions;
using BookRental.Domain.Interfaces.Repositories;
using BookRental.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace BookRental.Infrastructure.Repositories;

public class AuditableRepository<T>(BookRentalDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    : TrackableRepository<T>(dbContext, httpContextAccessor), IAuditableRepository<T>
    where T : class, IAuditable
{
    public Task UpdateAsync(T entity)
    {
        entity.UpdatedBy = CurrentUserId;
        entity.UpdatedAt = DateTimeOffset.UtcNow;
        
        _dbContext.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public async Task<int> UpdateAsync(string id, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls)
    {
        return await _dbContext.Set<T>()
            .Where(e => EF.Property<string>(e, "Id") == id)
            .ExecuteUpdateAsync(calls => AddUpdateFields(setPropertyCalls, calls));
    }

    private SetPropertyCalls<T> AddUpdateFields(
        Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> userSetPropertyCalls,
        SetPropertyCalls<T> calls)
    {
        var userUpdatedCalls = userSetPropertyCalls.Compile()(calls);
        
        return userUpdatedCalls
            .SetProperty(e => e.UpdatedBy, CurrentUserId)
            .SetProperty(e => e.UpdatedAt, DateTimeOffset.Now);
    }
}