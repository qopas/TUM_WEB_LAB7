using Application.DTOs.Destination;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Mediator.Destination.Queries.GetDestinationById;

public class GetDestinationByIdQueryHandler(IRepository<BookRental.Domain.Entities.Destination> destinationRepository)
    : IRequestHandler<GetDestinationByIdQuery, DestinationDto>
{
    public async Task<DestinationDto> Handle(GetDestinationByIdQuery request, CancellationToken cancellationToken)
    {
        var destination = await destinationRepository.GetByIdAsync(request.Id);
        return DestinationDto.FromEntity(destination);
    }
}