using BookRental.Domain.Interfaces;
using BookRental.Infrastructure.Extensions;
using BookRental.Domain.Common;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Rent.Commands.DeleteRent;

public class DeleteRentCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer localizer)
    : IRequestHandler<DeleteRentCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteRentCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.Rents.DeleteOrThrowAsync(request.Id, localizer);
        await unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }
}