using BookRental.Domain.Common;
using FluentValidation;
using MediatR;

namespace Application.Book.Commands.DeleteBook;

public class DeleteBookCommand : IRequest<Result<bool>>
{
    public required string Id { get; init; }
}
public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().NotNull().WithMessage("Id is required");
    }
}