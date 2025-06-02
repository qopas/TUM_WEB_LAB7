using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using BookRental.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BookRental.Infrastructure.Repositories;

public class BookRepository(BookRentalDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    : FullAuditableRepository<Book>(dbContext, httpContextAccessor), IBookRepository
{
}