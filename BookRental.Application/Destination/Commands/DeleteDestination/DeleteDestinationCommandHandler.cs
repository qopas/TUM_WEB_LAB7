using BookRental.Domain.Interfaces;
using BookRental.Infrastructure.Extensions;
using BookRental.Domain.Common;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Destination.Commands.DeleteDestination;

public class DeleteDestinationCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer localizer)
    : IRequestHandler<DeleteDestinationCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteDestinationCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.Destinations.DeleteOrThrowAsync(request.Id, localizer);
        await unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }
}