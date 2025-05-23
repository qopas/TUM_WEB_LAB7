using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces;
using BookRental.Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Genres.Commands.UpdateGenre;

public class UpdateGenreCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer localizer) : IRequestHandler<UpdateGenreCommand, bool>
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
        var existingGenre = await unitOfWork.Genres.GetByIdOrThrowAsync(request.Id, localizer);
        if (existingGenre == null)
        {
            return (existingGenre, false);
        }
        existingGenre.Name = request.Name;
        return (existingGenre, true);
    }
}