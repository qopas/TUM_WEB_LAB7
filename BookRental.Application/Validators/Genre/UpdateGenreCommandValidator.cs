using Application.Mediator.Genres.Commands.UpdateGenre;
using BookRental.Domain.Interfaces;
using FluentValidation;

namespace Application.Validators.Genre;

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