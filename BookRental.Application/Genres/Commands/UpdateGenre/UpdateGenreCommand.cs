using BookRental.Domain.Common;
using BookRental.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace Application.Genres.Commands.UpdateGenre;

public class UpdateGenreCommand : IRequest<Result<bool>>
{
    public required string Id { get; init; }
    public required string Name { get; init; }
}
public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(g => g.Id)
            .NotEmpty().WithMessage("Genre ID is required");

        RuleFor(g => g.Name)
            .NotEmpty().WithMessage("Genre name is required")
            .MaximumLength(50).WithMessage("Genre name cannot exceed 50 characters");
    }
}