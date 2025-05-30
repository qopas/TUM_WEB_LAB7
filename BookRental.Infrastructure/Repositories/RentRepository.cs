using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using BookRental.Infrastructure.Data;
using Microsoft.AspNetCore.Http;

namespace BookRental.Infrastructure.Repositories;

public class RentRepository(BookRentalDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    : AuditableRepository<Rent>(dbContext, httpContextAccessor), IRentRepository;