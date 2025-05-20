using Application.Mediator.Genres.Commands.CreateGenre;
using BookRental.Domain.Interfaces;
using FluentValidation;

namespace Application.Validators.Genre;

public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(g => g.Name)
            .NotEmpty().WithMessage("Genre name is required")
            .MaximumLength(50).WithMessage("Genre name cannot exceed 50 characters");
    }
}