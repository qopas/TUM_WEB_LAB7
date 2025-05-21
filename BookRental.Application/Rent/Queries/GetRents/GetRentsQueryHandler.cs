using Application.DTOs.Rent;
using BookRental.Domain.Interfaces;
using MediatR;

namespace Application.Rent.Queries.GetRents;

public class GetRentsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetRentsQuery, IEnumerable<RentDto>>
{
    public Task<IEnumerable<RentDto>> Handle(GetRentsQuery request, CancellationToken cancellationToken)
    {
        var rents = unitOfWork.Rents.GetAll();
        return Task.FromResult(rents.Select(RentDto.FromEntity));
    }
}