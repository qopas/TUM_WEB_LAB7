using BookRental.Domain.Interfaces;
using BookRental.Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Rent.Commands.DeleteRent;

public class DeleteRentCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer localizer) : IRequestHandler<DeleteRentCommand, bool>
{
    public async Task<bool> Handle(DeleteRentCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.Rents.DeleteOrThrowAsync(request.Id, localizer);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}