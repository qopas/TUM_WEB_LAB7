using Application.DTOs.Genre;
using MediatR;

namespace Application.Genres.Queries.GetGenreById;

public class GetGenreByIdQuery : IRequest<GenreDto>
{
    public required string Id { get; init; }
}