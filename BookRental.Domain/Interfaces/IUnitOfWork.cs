using BookRental.Domain.Interfaces.Repositories;

namespace BookRental.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IBookRepository Books { get; }
    IGenreRepository Genres { get; }
    ICustomerRepository Customers { get; }
    IRentRepository Rents { get; }
        
    Task<int> SaveChangesAsync();
}