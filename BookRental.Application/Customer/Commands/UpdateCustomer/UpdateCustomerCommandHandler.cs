using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Customer.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCustomerCommand, bool>
{
    public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var (existingCustomer, handle) = await Convert(request);
        if (!handle) return false;

        await unitOfWork.Customers.Update(existingCustomer);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    private async Task<(BookRental.Domain.Entities.Customer existingCustomer, bool handle)> Convert(UpdateCustomerCommand request)
    {
        var existingCustomer = await unitOfWork.Customers.GetByIdAsync(request.Id);
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