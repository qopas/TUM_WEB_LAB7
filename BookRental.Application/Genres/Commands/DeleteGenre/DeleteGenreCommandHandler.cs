using BookRental.Domain.Interfaces;
using BookRental.Infrastructure.Extensions;
using MediatR;

namespace Application.Genres.Commands.DeleteGenre;

public class DeleteGenreCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<DeleteGenreCommand, bool>
{
    public async Task<bool> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.Genres.DeleteOrThrowAsync(request.Id);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}