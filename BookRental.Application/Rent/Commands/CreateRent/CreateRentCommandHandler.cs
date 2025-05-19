
using Application.DTOs.Rent;
using Application.Mapping;
using BookRental.Domain.Enums;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Rent.Commands.CreateRent;

public class CreateRentCommandHandler(IRepository<BookRental.Domain.Entities.Rent> rentRepository) : IRequestHandler<CreateRentCommand, RentDto>
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

        var createdRent = await rentRepository.AddAsync(rent);
        return createdRent.ToDto();
    }
}