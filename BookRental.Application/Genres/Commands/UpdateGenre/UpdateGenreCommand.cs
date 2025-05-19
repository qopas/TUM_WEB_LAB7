using MediatR;

namespace Application.Mediator.Genres.Commands.UpdateGenre;

public class UpdateGenreCommand : IRequest<bool>
{
    public string Id { get; set; }
    public string Name { get; set; }
}