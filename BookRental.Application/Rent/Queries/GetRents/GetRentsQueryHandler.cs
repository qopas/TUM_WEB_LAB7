using Application.DTOs.Rent;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Rent.Queries.GetRents;

public class GetRentsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetRentsQuery, IEnumerable<RentDto>>
{
    public async Task<IEnumerable<RentDto>> Handle(GetRentsQuery request, CancellationToken cancellationToken)
    {
        var rents = unitOfWork.Rents.GetAll();
        return RentDto.FromEntityList(rents);
    }
}