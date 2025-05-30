using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using BookRental.Infrastructure.Data;
using Microsoft.AspNetCore.Http;

namespace BookRental.Infrastructure.Repositories;

public class GenreRepository(BookRentalDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    : TrackableRepository<Genre>(dbContext, httpContextAccessor), IGenreRepository;