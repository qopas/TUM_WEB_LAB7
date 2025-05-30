using BookRental.Domain.Entities.Base;

namespace BookRental.Domain.Interfaces.Repositories;

public interface IFullAuditableRepository<T> : IAuditableRepository<T> where T : class, IFullAuditable
{
    Task SoftDeleteAsync(T entity);
    Task<int> SoftDeleteAsync(string id);
    Task<bool> RestoreAsync(string id);
    IQueryable<T> GetAllIncludingDeleted();
    IQueryable<T> GetOnlyDeleted();
    IQueryable<T> GetByIdIncludingDeleted(string id);
}
