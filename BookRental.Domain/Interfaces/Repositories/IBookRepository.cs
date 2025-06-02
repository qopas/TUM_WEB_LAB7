using BookRental.Domain.Entities;

namespace BookRental.Domain.Interfaces.Repositories;

public interface IBookRepository : IFullAuditableRepository<Book>
{
}