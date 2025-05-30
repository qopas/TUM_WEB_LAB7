using BookRental.Domain.Entities;

namespace BookRental.Domain.Interfaces.Repositories;

public interface IRefreshTokenRepository : IFullAuditableRepository<RefreshToken>
{
}