using BookRental.Domain.Interfaces;
using BookRental.Infrastructure.Extensions;
using MediatR;

namespace Application.Customer.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCustomerCommand, bool>
{
    public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.Customers.DeleteOrThrowAsync(request.Id);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}