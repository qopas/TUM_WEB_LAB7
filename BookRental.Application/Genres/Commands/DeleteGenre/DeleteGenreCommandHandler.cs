using BookRental.Domain.Interfaces;
using BookRental.Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Genres.Commands.DeleteGenre;

public class DeleteGenreCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer localizer): IRequestHandler<DeleteGenreCommand, bool>
{
    public async Task<bool> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.Genres.DeleteOrThrowAsync(request.Id, localizer);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}