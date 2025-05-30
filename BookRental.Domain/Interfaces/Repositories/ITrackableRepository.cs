using BookRental.Domain.Entities.Base;

namespace BookRental.Domain.Interfaces.Repositories;

public interface ITrackableRepository<T> : IRepository<T> where T : class, ITrackable
{
    Task<T> CreateAsync(T entity);
    Task HardDeleteAsync(T entity);
    Task HardDeleteAsync(string id);
}