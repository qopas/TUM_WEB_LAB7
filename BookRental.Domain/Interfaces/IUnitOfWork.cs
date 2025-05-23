using BookRental.Domain.Interfaces.Repositories;

namespace BookRental.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IBookRepository Books { get; }
    IGenreRepository Genres { get; }
    ICustomerRepository Customers { get; }
    IRefreshTokenRepository RefreshTokens { get; }
    IRentRepository Rents { get; }
    IDestinationRepository Destinations { get; }
        
    Task<int> SaveChangesAsync();
}