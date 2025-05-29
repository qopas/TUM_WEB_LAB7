using BookRental.Domain.Interfaces;
using BookRental.Domain.Common;
using MediatR;

namespace Application.Customer.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateCustomerCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var rowsAffected = await unitOfWork.Customers.UpdateAsync(request.Id, setters => setters
            .SetProperty(c => c.FirstName, request.FirstName)
            .SetProperty(c => c.LastName, request.LastName)
            .SetProperty(c => c.Address, request.Address)
            .SetProperty(c => c.City, request.City));
        
        return rowsAffected.ToUpdateResult<BookRental.Domain.Entities.Customer>(request.Id);
    }
}
