using MediatR;

namespace Application.Mediator.Genres.Commands.DeleteGenre;

public class DeleteGenreCommand : IRequest<bool>
{
    public string Id { get; set; }
}