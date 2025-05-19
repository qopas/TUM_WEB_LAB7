using Application.DTOs.Rent;
using MediatR;

namespace Application.Mediator.Rent.Queries.GetRentById;

public class GetRentByIdQuery : IRequest<RentDto>
{
    public string Id { get; set; }
}
