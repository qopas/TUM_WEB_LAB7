using Application.DTOs.Book;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Book.Queries.GetBookById;

public class GetBookByIdQuery : IRequest<BookDto>
{
    public required string Id { get; init; }
}
public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
{
    public GetBookByIdQueryValidator(IStringLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty().NotNull().WithMessage(localizer["idRequired"]);
    }
}