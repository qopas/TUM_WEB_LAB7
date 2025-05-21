using Application.Genres.Queries.GetGenreById;
using FluentValidation;
using MediatR;

namespace Application.Rent.Commands.DeleteRent;

public class DeleteRentCommand : IRequest<bool>
{
    public required string Id { get; init; }
}
public class DeleteRentCommandValidator : AbstractValidator<GetGenreByIdQuery>
{
    public DeleteRentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().NotNull().WithMessage("Id is required");
    }
}