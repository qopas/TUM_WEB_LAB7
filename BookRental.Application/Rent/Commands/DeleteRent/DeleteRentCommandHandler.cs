using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Rent.Commands.DeleteRent;

public class DeleteRentCommandHandler(IRepository<BookRental.Domain.Entities.Rent> rentRepository) : IRequestHandler<DeleteRentCommand, bool>
{
    public async Task<bool> Handle(DeleteRentCommand request, CancellationToken cancellationToken)
    {
        var rent = await rentRepository.GetByIdAsync(request.Id);
        if (rent == null)
        {
            return false;
        }

        await rentRepository.DeleteAsync(request.Id);
        return true;
    }
}