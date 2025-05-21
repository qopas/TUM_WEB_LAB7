using Application.DTOs.Destination;
using FluentValidation;
using MediatR;

namespace Application.Destination.Queries.GetDestinationById;

public class GetDestinationByIdQuery : IRequest<DestinationDto>
{
    public required string Id { get; init; }
}
public class GetDestinationByIdQueryValidator : AbstractValidator<GetDestinationByIdQuery>
{
    public GetDestinationByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().NotNull().WithMessage("Id is required");
    }
}