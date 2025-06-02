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
    public async Task<int> UpdateAsync(string id,
        Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setProperties)
    {
        return await _dbContext.Set<T>()
            .Where(x => x.Id.Equals(id))
            .ExecuteUpdateAsync(CombinedExpression(setProperties));
    }


    private Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> CombinedExpression(
        Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> s1)
    {
        Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> s2 = x =>
            x.SetProperty(xx => xx.UpdatedAt, DateTime.Now)
                .SetProperty(xx => xx.UpdatedBy, CurrentUserId);

        var parameter = Expression.Parameter(typeof(SetPropertyCalls<T>), "x");

        return
            Expression.Lambda<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>>(
                Expression.Invoke(s2, Expression.Invoke(s1, parameter)), parameter);
    }
}