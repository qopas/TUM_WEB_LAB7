using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using BookRental.Infrastructure.Data;

namespace BookRental.Infrastructure.Repositories;

public class DestinationRespository(BookRentalDbContext dbContext)
    : Repository<Destination>(dbContext), IDestinationRepository;