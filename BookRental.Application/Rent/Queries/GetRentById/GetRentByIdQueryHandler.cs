using Application.DTOs.Rent;
using BookRental.Domain.Interfaces;
using BookRental.Infrastructure.Extensions;
using MediatR;

namespace Application.Rent.Queries.GetRentById;

public class GetRentByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetRentByIdQuery, RentDto>
{
    public async Task<RentDto> Handle(GetRentByIdQuery request, CancellationToken cancellationToken)
    {
        var rent = await unitOfWork.Rents.GetByIdOrThrowAsync(request.Id);
        return RentDto.FromEntity(rent);
    }
}