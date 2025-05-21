using Application.Mediator.Genres.Commands.UpdateGenre;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Genres.Commands.UpdateGenre;

public class UpdateGenreCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateGenreCommand, bool>
{
    public async Task<bool> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var existingGenre = await unitOfWork.Genres.GetByIdAsync(request.Id);
        if (existingGenre == null)
        {
            return false;
        }
        existingGenre.Name = request.Name;
        await unitOfWork.Genres.Update(existingGenre);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}