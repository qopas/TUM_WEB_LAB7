using Microsoft.EntityFrameworkCore;

namespace BookRental.Infrastructure.Data;

public class BookRentalDbContext : DbContext
{
    public BookRentalDbContext(DbContextOptions<BookRentalDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}