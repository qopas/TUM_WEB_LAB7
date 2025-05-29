using BookRental.Domain.Interfaces;
using BookRental.Infrastructure.Extensions;
using BookRental.Domain.Common;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Genres.Commands.DeleteGenre;

public class DeleteGenreCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer localizer): IRequestHandler<DeleteGenreCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.Genres.DeleteOrThrowAsync(request.Id, localizer);
        await unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }
}