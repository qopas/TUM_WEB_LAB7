using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace BookRental.Domain.Interfaces.Repositories;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(string id);
    IQueryable<T> GetAll();
    IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task<int> UpdateAsync(string id, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls);
    Task DeleteAsync(T entity);
    Task DeleteAsync(string id);
}