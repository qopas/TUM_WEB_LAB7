using Application.DTOs.Rent;
using BookRental.Domain.Interfaces;
using BookRental.Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Rent.Queries.GetRentById;

public class GetRentByIdQueryHandler(IUnitOfWork unitOfWork, IStringLocalizer localizer) : IRequestHandler<GetRentByIdQuery, RentDto>
{
    public async Task<RentDto> Handle(GetRentByIdQuery request, CancellationToken cancellationToken)
    {
        var rent = await unitOfWork.Rents.GetByIdOrThrowAsync(request.Id, localizer);
        return RentDto.FromEntity(rent);
    }
}