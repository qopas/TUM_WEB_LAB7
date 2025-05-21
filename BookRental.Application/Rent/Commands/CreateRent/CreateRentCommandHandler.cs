using Application.DTOs.Rent;
using BookRental.Domain.Enums;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Rent.Commands.CreateRent;

public class CreateRentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateRentCommand, RentDto>
{
    public async Task<RentDto> Handle(CreateRentCommand request, CancellationToken cancellationToken)
    {
        var rent = new BookRental.Domain.Entities.Rent
        {
            BookId = request.BookId,
            CustomerId = request.CustomerId,
            DestinationId = request.DestinationId,
            RentDate = request.RentDate,
            DueDate = request.DueDate,
            Status = RentStatus.Active
        };

        var createdRent = await unitOfWork.Rents.AddAsync(rent);
        await unitOfWork.SaveChangesAsync();
        return RentDto.FromEntity(createdRent);
    }
}