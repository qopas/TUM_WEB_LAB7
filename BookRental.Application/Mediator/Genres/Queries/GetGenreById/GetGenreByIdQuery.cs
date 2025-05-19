using Application.DTOs.Genre;
using MediatR;

namespace Application.Mediator.Genres.Queries.GetGenreById;

public class GetGenreByIdQuery : IRequest<GenreDto>
{
    public string Id { get; set; }
}