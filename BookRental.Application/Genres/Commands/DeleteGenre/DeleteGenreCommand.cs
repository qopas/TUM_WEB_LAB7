using MediatR;

namespace Application.Genres.Commands.DeleteGenre;

public class DeleteGenreCommand : IRequest<bool>
{
    public required string Id { get; init; }
}