using BookRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookRental.Infrastructure.Data;

public class BookRentalDbContext : DbContext
{
    DbSet<Genre> Genres { get; set; }  
    DbSet<Book> Books { get; set; }
    DbSet<Customer> Customers { get; set; }
    public BookRentalDbContext(DbContextOptions<BookRentalDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookRentalDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}