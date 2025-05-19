using Application.DTOs.Genre;
using MediatR;

namespace Application.Mediator.Genres.Commands.CreateGenre;

public class CreateGenreCommand : IRequest<GenreDto>
{
    public string Name { get; set; }
}
