using Application.DTOs.Genre;
using BookRental.Domain.Interfaces;
using BookRental.Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Genres.Queries.GetGenreById;

public class GetGenreByIdQueryHandler(IUnitOfWork unitOfWork, IStringLocalizer localizer) : IRequestHandler<GetGenreByIdQuery, GenreDto>
{
    public async Task<GenreDto> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
    {
        var genre = await unitOfWork.Genres.GetByIdOrThrowAsync(request.Id, localizer);
        return GenreDto.FromEntity(genre);
    }
}