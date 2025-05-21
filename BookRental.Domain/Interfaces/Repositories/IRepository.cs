using System.Linq.Expressions;

namespace BookRental.Domain.Interfaces.Repositories;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(string id);
    IQueryable<T> GetAll();
    IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task Delete(string id);
}