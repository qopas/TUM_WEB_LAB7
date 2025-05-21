using BookRental.Domain.Interfaces;
using MediatR;

namespace Application.Genres.Commands.DeleteGenre;

public class DeleteGenreCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<DeleteGenreCommand, bool>
{
    public async Task<bool> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await unitOfWork.Genres.GetByIdAsync(request.Id);
        if (genre == null)
        {
            return false;
        }
        await unitOfWork.Genres.DeleteAsync(genre);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}