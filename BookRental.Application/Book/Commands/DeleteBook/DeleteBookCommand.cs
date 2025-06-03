using BookRental.Domain.Common;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Book.Commands.DeleteBook;

public class DeleteBookCommand : IRequest<Result<bool>>
{
    public required string Id { get; init; }
}
public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator(IStringLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty().NotNull().WithMessage(localizer["idRequired"]);
    }
}