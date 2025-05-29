using BookRental.Domain.Common;
using FluentValidation;
using MediatR;

namespace Application.Genres.Commands.DeleteGenre;

public class DeleteGenreCommand : IRequest<Result<bool>>
{
    public required string Id { get; init; }
}
public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
{
    public DeleteGenreCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().NotNull().WithMessage("Id is required");
    }
}