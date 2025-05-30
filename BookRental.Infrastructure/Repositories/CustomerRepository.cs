using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using BookRental.Infrastructure.Data;
using Microsoft.AspNetCore.Http;

namespace BookRental.Infrastructure.Repositories;

public class CustomerRepository(BookRentalDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    : FullAuditableRepository<Customer>(dbContext, httpContextAccessor), ICustomerRepository;