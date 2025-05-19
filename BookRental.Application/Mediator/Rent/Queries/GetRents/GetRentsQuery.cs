using Application.DTOs.Rent;
using MediatR;

namespace Application.Mediator.Rent.Queries.GetRents;

public class GetRentsQuery : IRequest<IEnumerable<RentDto>>
{
}