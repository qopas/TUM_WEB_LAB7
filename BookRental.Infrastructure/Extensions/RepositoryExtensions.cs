using System.Linq.Expressions;
using BookRental.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BookRental.Infrastructure.Extensions;

public static class RepositoryExtensions
{
    public static async Task<T> GetByIdOrThrowAsync<T>(
        this IRepository<T> repository,
        string id,
        IStringLocalizer localizer) where T : class
    {
        var entity = await repository.GetByIdAsync(id);

        if (entity == null)
        {
            var entityName = typeof(T).Name;
            throw new KeyNotFoundException(localizer["Entity_NotFound", entityName, id]);
        }

        return entity;
    }

    public static async Task DeleteOrThrowAsync<T>(
        this IRepository<T> repository,
        string id,
        IStringLocalizer localizer) where T : class
    {
        var entity = await repository.GetByIdOrThrowAsync(id, localizer);
        await repository.DeleteAsync(entity);
    }
}
