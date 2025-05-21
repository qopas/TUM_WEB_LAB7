using Application.Genres.Commands.UpdateGenre;
using Application.Mediator.Genres.Commands.UpdateGenre;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Repositories;
using BookRental.Infrastructure.Extensions;
using MediatR;

namespace Application.Mediator.Genres.Commands.UpdateGenre;

public class UpdateGenreCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateGenreCommand, bool>
{
    public async Task<bool> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var (existingGenre, handle) = await Convert(request);
        if (!handle) return false;
        await unitOfWork.Genres.UpdateAsync(existingGenre);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    private async Task<(Genre? existingGenre, bool handle)> Convert(UpdateGenreCommand request)
    {
        var existingGenre = await unitOfWork.Genres.GetByIdOrThrowAsync(request.Id);
        if (existingGenre == null)
        {
            return (existingGenre, false);
        }
        existingGenre.Name = request.Name;
        return (existingGenre, true);
    }
}