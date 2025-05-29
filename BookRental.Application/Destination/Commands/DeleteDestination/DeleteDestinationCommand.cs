using BookRental.Domain.Common;
using FluentValidation;
using MediatR;

namespace Application.Destination.Commands.DeleteDestination;

public class DeleteDestinationCommand : IRequest<Result<bool>>
{
    public required string Id { get; init; }
}
public class DeleteDestinationCommandValidator : AbstractValidator<DeleteDestinationCommand>
{
    public DeleteDestinationCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().NotNull().WithMessage("Id is required");
    }
}