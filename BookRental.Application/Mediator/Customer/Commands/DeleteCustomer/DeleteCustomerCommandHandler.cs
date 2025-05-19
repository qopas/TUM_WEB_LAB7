using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Customer.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler(IRepository<BookRental.Domain.Entities.Customer> customerRepository)
    : IRequestHandler<DeleteCustomerCommand, bool>
{
    public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(request.Id);
        if (customer == null)
        {
            return false;
        }

        await customerRepository.DeleteAsync(request.Id);
        return true;
    }
}