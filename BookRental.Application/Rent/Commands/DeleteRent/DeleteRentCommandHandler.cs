using BookRental.Domain.Interfaces;
using MediatR;

namespace Application.Rent.Commands.DeleteRent;

public class DeleteRentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteRentCommand, bool>
{
    public async Task<bool> Handle(DeleteRentCommand request, CancellationToken cancellationToken)
    {
        var rent = await unitOfWork.Rents.GetByIdAsync(request.Id);
        if (rent == null)
        {
            return false;
        }

        await unitOfWork.Rents.Delete(rent);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}