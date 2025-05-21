using Application.DTOs.Rent;
using MediatR;

namespace Application.Rent.Queries.GetRents;

public class GetRentsQuery : IRequest<IEnumerable<RentDto>>
{
}