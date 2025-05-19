using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using BookRental.Infrastructure.Data;

namespace BookRental.Infrastructure.Repositories;

public class RentRepository : Repository<Rent>, IRentRepository
{
    public RentRepository(BookRentalDbContext dbContext) : base(dbContext)
    {
    }
}