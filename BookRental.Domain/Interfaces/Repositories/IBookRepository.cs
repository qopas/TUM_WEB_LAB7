using BookRental.Domain.Entities;

namespace BookRental.Domain.Interfaces.Repositories;

public interface IBookRepository : IFullAuditableRepository<Book>
{
    Task<IReadOnlyList<Book>> GetBooksByGenreAsync(string genreId);
    Task<IReadOnlyList<Book>> GetAvailableBooksAsync();
    Task<Book> GetBookWithDetailsAsync(string id);
}