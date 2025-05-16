using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using BookRental.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookRental.Infrastructure.Repositories;

public class BookRepository : Repository<Book>, IBookRepository
{
    public BookRepository(BookRentalDbContext dbContext) 
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<Book>> GetBooksByGenreAsync(string genreId)
    {
        return await _dbContext.Books
            .Where(b => b.GenreId == genreId)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Book>> GetAvailableBooksAsync()
    {
        return await _dbContext.Books
            .Where(b => b.AvailableQuantity > 0)
            .ToListAsync();
    }
    
    public async Task<Book> GetBookWithDetailsAsync(string id)
    {
        return await _dbContext.Books
            .Include(b => b.Genre)
            .Include(b => b.Rents)
            .ThenInclude(r => r.Customer)
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}