using System.Security.Claims;
using BookRental.Domain.Entities.Base;
using BookRental.Domain.Interfaces.Repositories;
using BookRental.Infrastructure.Data;
using Microsoft.AspNetCore.Http;

namespace BookRental.Infrastructure.Repositories;

public class TrackableRepository<T>(BookRentalDbContext dbContext, IHttpContextAccessor httpContextAccessor) : Repository<T>(dbContext), ITrackableRepository<T> 
    where T : class, ITrackable
{
    protected string? CurrentUserId => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public async Task<T> CreateAsync(T entity)
    {
        entity.CreatedBy = CurrentUserId;
        await _dbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public Task HardDeleteAsync(T entity)
    {
        return HardRemoveAsync(entity);
    }
        
    public Task HardDeleteAsync(string id)
    {
        return HardRemoveAsync(id);
    }
}