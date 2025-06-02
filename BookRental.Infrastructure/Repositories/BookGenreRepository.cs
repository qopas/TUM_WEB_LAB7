using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using BookRental.Infrastructure.Data;
using Microsoft.AspNetCore.Http;

namespace BookRental.Infrastructure.Repositories;

public class BookGenreRepository(BookRentalDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    : FullAuditableRepository<BookGenre>(dbContext, httpContextAccessor), IBookGenreRepository;