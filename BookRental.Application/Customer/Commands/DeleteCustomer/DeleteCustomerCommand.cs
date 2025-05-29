using BookRental.Domain.Common;
using FluentValidation;
using MediatR;

namespace Application.Customer.Commands.DeleteCustomer;

public class DeleteCustomerCommand : IRequest<Result<bool>>
{
    public required string Id { get; init; }
}
public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().NotNull().WithMessage("Id is required");
    }
}