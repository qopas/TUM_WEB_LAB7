using Application.DTOs.Rent;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Rent.Queries.GetRentById;

public class GetRentByIdQueryHandler(IRepository<BookRental.Domain.Entities.Rent> rentRepository) : IRequestHandler<GetRentByIdQuery, RentDto>
{
    public async Task<RentDto> Handle(GetRentByIdQuery request, CancellationToken cancellationToken)
    {
        var rent = await rentRepository.GetByIdAsync(request.Id);
        return RentDto.FromEntity(rent);
    }
}