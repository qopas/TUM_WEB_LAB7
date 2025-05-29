using BookRental.Domain.Interfaces;
using BookRental.Domain.Common;
using MediatR;

namespace Application.Genres.Commands.UpdateGenre;

public class UpdateGenreCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateGenreCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var rowsAffected = await unitOfWork.Genres.UpdateAsync(request.Id, setters => setters
            .SetProperty(g => g.Name, request.Name));
        
        return rowsAffected.ToUpdateResult<BookRental.Domain.Entities.Genre>(request.Id);
    }
}
