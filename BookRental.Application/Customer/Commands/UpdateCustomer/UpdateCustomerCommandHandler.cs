using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Customer.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler(IRepository<BookRental.Domain.Entities.Customer> customerRepository)
    : IRequestHandler<UpdateCustomerCommand, bool>
{


    public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var existingCustomer = await customerRepository.GetByIdAsync(request.Id);
        if (existingCustomer == null)
        {
            return false;
        }

        existingCustomer.FirstName = request.FirstName;
        existingCustomer.LastName = request.LastName;
        existingCustomer.Email = request.Email;
        existingCustomer.PhoneNumber = request.PhoneNumber;
        existingCustomer.Address = request.Address;
        existingCustomer.City = request.City;

        await customerRepository.UpdateAsync(existingCustomer);
        return true;
    }
}