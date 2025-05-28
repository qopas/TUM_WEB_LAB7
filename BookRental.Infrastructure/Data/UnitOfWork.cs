using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Repositories;
using BookRental.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookRental.Infrastructure.Data;

public class UnitOfWork(
    BookRentalDbContext dbContext,
    IBookRepository bookRepository,
    IGenreRepository genreRepository,
    IDestinationRepository destinationRepository,
    ICustomerRepository customerRepository,
    IRentRepository rentRepository,
    IRefreshTokenRepository refreshTokenRepository)
    : IUnitOfWork
{
    private IDbContextTransaction _transaction;
    
    private bool _disposed = false;
    public IBookRepository Books { get; } = bookRepository;
    public IGenreRepository Genres { get; } = genreRepository;
    public IDestinationRepository Destinations { get; } = destinationRepository;
    public ICustomerRepository Customers { get; } = customerRepository;
    public IRefreshTokenRepository RefreshTokens { get; } = refreshTokenRepository;
    public IRentRepository Rents { get; } = rentRepository;

    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }
    
    public async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> action)
    {
        return await dbContext.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
        {
            using var transaction = BeginTransactionAsync();
            try
            {
                var result = await action();
                await CommitTransactionAsync();
                return result;
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
        });
    }
    private async Task BeginTransactionAsync()
    {
        _transaction = await dbContext.Database.BeginTransactionAsync();
    }

    private async Task CommitTransactionAsync()
    {
        try
        {
            await _transaction.CommitAsync();
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    private async Task RollbackTransactionAsync()
    {
        try
        {
            await _transaction.RollbackAsync();
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
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