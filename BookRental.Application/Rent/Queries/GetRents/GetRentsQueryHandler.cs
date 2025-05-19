using Application.DTOs.Rent;
using Application.Mapping;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Rent.Queries.GetRents;

public class GetRentsQueryHandler(IRepository<BookRental.Domain.Entities.Rent> rentRepository)
    : IRequestHandler<GetRentsQuery, IEnumerable<RentDto>>
{
    public async Task<IEnumerable<RentDto>> Handle(GetRentsQuery request, CancellationToken cancellationToken)
    {
        var rents = await rentRepository.GetAllAsync();
        return rents.ToDtoList();
    }
}