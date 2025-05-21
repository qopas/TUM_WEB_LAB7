using Application.DTOs.Rent;
using BookRental.Domain.Interfaces;
using MediatR;

namespace Application.Rent.Queries.GetRentById;

public class GetRentByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetRentByIdQuery, RentDto>
{
    public async Task<RentDto> Handle(GetRentByIdQuery request, CancellationToken cancellationToken)
    {
        var rent = await unitOfWork.Rents.GetByIdAsync(request.Id);
        return RentDto.FromEntity(rent);
    }
}