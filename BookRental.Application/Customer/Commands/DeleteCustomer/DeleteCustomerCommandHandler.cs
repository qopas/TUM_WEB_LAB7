using BookRental.Domain.Interfaces;
using MediatR;

namespace Application.Customer.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCustomerCommand, bool>
{
    public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.Customers.GetByIdAsync(request.Id);
        if (customer == null)
        {
            return false;
        }

        await unitOfWork.Customers.DeleteAsync(customer);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}