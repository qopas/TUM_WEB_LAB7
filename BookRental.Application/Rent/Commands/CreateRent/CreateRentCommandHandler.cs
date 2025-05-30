using Application.DTOs.Rent;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Common;
using BookRental.Domain.Entities.Models;
using BookRental.Domain.Enums;
using MediatR;

namespace Application.Rent.Commands.CreateRent;

public class CreateRentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateRentCommand, Result<RentDto>>
{
    public async Task<Result<RentDto>> Handle(CreateRentCommand request, CancellationToken cancellationToken)
    {
        var rentModel = new RentModel
        {
            BookId = request.BookId,
            CustomerId = request.CustomerId,
            DestinationId = request.DestinationId,
            RentDate = request.RentDate,
            DueDate = request.DueDate,
            Status = RentStatus.Active
        };
        
        var rentResult = BookRental.Domain.Entities.Rent.Create(rentModel);
        if (!rentResult.IsSuccess)
            return Result<RentDto>.Failure(rentResult.Errors);

        var createdRent = await unitOfWork.Rents.CreateAsync(rentResult.Value);
        await unitOfWork.SaveChangesAsync();
        return Result<RentDto>.Success(RentDto.FromEntity(createdRent));
    }
}
