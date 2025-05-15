using BookRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookRental.Infrastructure.Data;

public class BookRentalDbContext : DbContext
{
   public DbSet<Genre> Genres { get; set; }  
   public DbSet<Book> Books { get; set; }
   public DbSet<Customer> Customers { get; set; }
   public DbSet<Destination> Destinations { get; set; }
   public DbSet<Rent> Rents { get; set; }
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