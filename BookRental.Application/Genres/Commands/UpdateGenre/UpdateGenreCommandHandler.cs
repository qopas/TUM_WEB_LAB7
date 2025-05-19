using Application.Mediator.Genres.Commands.UpdateGenre;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Genres.Commands.UpdateGenre;

public class UpdateGenreCommandHandler(IRepository<Genre> genreRepository) : IRequestHandler<UpdateGenreCommand, bool>
{
    public async Task<bool> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var existingGenre = await genreRepository.GetByIdAsync(request.Id);
        if (existingGenre == null)
        {
            return false;
        }
        existingGenre.Name = request.Name;
        await genreRepository.UpdateAsync(existingGenre);
        return true;
    }
}