using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Genres.Commands.DeleteGenre;

public class DeleteGenreCommandHandler(IRepository<Genre> genreRepository): IRequestHandler<DeleteGenreCommand, bool>
{
    public async Task<bool> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await genreRepository.GetByIdAsync(request.Id);
        if (genre == null)
        {
            return false;
        }
        await genreRepository.DeleteAsync(request.Id);
        return true;
    }
}