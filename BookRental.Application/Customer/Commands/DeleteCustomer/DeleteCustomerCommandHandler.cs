using BookRental.Domain.Interfaces;
using BookRental.Infrastructure.Extensions;
using BookRental.Domain.Common;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Customer.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer localizer)
    : IRequestHandler<DeleteCustomerCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.Customers.DeleteOrThrowAsync(request.Id, localizer);
        await unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true);
    }
}