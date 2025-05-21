using Application.DTOs.Rent;
using MediatR;

namespace Application.Rent.Queries.GetRentById;

public class GetRentByIdQuery : IRequest<RentDto>
{
    public required  string Id { get; init; }
}
