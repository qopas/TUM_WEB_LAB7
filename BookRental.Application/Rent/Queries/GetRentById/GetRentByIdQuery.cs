using Application.DTOs.Rent;
using FluentValidation;
using MediatR;

namespace Application.Rent.Queries.GetRentById;

public class GetRentByIdQuery : IRequest<RentDto>
{
    public required string Id { get; init; }
}
public class GetRentByIdQueryValidator : AbstractValidator<GetRentByIdQuery>
{
    public GetRentByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().NotNull().WithMessage("Id is required");
    }
}
