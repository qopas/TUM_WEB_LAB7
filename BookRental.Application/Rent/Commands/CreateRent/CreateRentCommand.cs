using Application.DTOs.Rent;
using MediatR;

namespace Application.Mediator.Rent.Commands.CreateRent;

public class CreateRentCommand : IRequest<RentDto>
{
    public string BookId { get; set; }
    public string CustomerId { get; set; }
    public string DestinationId { get; set; }
    public DateTime RentDate { get; set; }
    public DateTime DueDate { get; set; }
}