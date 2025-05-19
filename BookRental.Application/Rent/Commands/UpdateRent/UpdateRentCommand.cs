using BookRental.Domain.Enums;
using MediatR;

namespace Application.Mediator.Rent.Commands.UpdateRent;

public class UpdateRentCommand : IRequest<bool>
{
    public string Id { get; set; }
    public string BookId { get; set; }
    public string CustomerId { get; set; }
    public string DestinationId { get; set; }
    public DateTime RentDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public RentStatus Status { get; set; }
}