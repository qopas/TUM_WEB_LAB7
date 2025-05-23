using BookRental.Domain.Interfaces;
using BookRental.Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Customer.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer localizer)
    : IRequestHandler<UpdateCustomerCommand, bool>
{
    public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var (existingCustomer, handle) = await Convert(request);
        if (!handle) return false;

        await unitOfWork.Customers.UpdateAsync(existingCustomer);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    private async Task<(BookRental.Domain.Entities.Customer existingCustomer, bool handle)> Convert(UpdateCustomerCommand request)
    {
        var existingCustomer = await unitOfWork.Customers.GetByIdOrThrowAsync(request.Id, localizer);
        if (existingCustomer == null)
        {
            return (existingCustomer, false);
        }

        existingCustomer.FirstName = request.FirstName;
        existingCustomer.LastName = request.LastName;
        existingCustomer.Email = request.Email;
        existingCustomer.PhoneNumber = request.PhoneNumber;
        existingCustomer.Address = request.Address;
        existingCustomer.City = request.City;
        return (existingCustomer, true);
    }
}