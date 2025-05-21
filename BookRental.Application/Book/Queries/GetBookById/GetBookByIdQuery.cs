using Application.DTOs.Book;
using FluentValidation;
using MediatR;

namespace Application.Book.Queries.GetBookById;

public class GetBookByIdQuery : IRequest<BookDto>
{
    public required string Id { get; init; }
}
public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
{
    public GetBookByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().NotNull().WithMessage("Id is required");
    }
}