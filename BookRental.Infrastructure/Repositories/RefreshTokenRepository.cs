using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using BookRental.Infrastructure.Data;
using Microsoft.AspNetCore.Http;

namespace BookRental.Infrastructure.Repositories;

public class RefreshTokenRepository(BookRentalDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    : FullAuditableRepository<RefreshToken>(dbContext, httpContextAccessor), IRefreshTokenRepository;