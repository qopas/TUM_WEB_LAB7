using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Repositories;
using BookRental.Infrastructure.Repositories;

namespace BookRental.Infrastructure.Data;

public class UnitOfWork(
    BookRentalDbContext dbContext,
    IBookRepository bookRepository,
    IGenreRepository genreRepository,
    ICustomerRepository customerRepository,
    IRentRepository rentRepository)
    : IUnitOfWork
{
    private bool _disposed = false;
    public IBookRepository Books { get; } = bookRepository;
    public IGenreRepository Genres { get; } = genreRepository;
    public ICustomerRepository Customers { get; } = customerRepository;
    public IRentRepository Rents { get; } = rentRepository;

    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed || !disposing) return;
        dbContext.Dispose();
        _disposed = true;
    }
}