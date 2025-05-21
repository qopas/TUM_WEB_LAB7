using Application.DTOs.Destination;
using BookRental.Domain.Interfaces;
using MediatR;

namespace Application.Destination.Queries.GetDestinationById;

public class GetDestinationByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetDestinationByIdQuery, DestinationDto>
{
    public async Task<DestinationDto> Handle(GetDestinationByIdQuery request, CancellationToken cancellationToken)
    {
        var destination = await unitOfWork.Destinations.GetByIdAsync(request.Id);
        return DestinationDto.FromEntity(destination);
    }
}