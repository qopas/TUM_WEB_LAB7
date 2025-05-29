using Application.DTOs.Destination;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Common;
using BookRental.Domain.Entities.Models;
using MediatR;

namespace Application.Destination.Commands.CreateDestination;

public class CreateDestinationCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateDestinationCommand, Result<DestinationDto>>
{
    public async Task<Result<DestinationDto>> Handle(CreateDestinationCommand request, CancellationToken cancellationToken)
    {
        var destinationModel = new DestinationModel
        {
            Name = request.Name,
            Address = request.Address,
            City = request.City,
            ContactPerson = request.ContactPerson,
            PhoneNumber = request.PhoneNumber
        };

        var destinationResult = BookRental.Domain.Entities.Destination.Create(destinationModel);
        if (!destinationResult.IsSuccess)
            return Result<DestinationDto>.Failure(destinationResult.Errors);

        var createdDestination = await unitOfWork.Destinations.AddAsync(destinationResult.Value);
        await unitOfWork.SaveChangesAsync();
        return Result<DestinationDto>.Success(DestinationDto.FromEntity(createdDestination));
    }
}
