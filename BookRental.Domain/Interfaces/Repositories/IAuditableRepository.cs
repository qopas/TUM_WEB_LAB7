using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace BookRental.Domain.Interfaces.Repositories;

public interface IAuditableRepository<T> : ITrackableRepository<T> where T : class, IAuditable
{
    Task UpdateAsync(T entity);
    Task<int> UpdateAsync(string id, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls);
}
