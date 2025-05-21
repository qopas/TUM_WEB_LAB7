using Application.DTOs.Genre;
using FluentValidation;
using MediatR;

namespace Application.Mediator.Genres.Commands.CreateGenre;

public class CreateGenreCommand : IRequest<GenreDto>
{
    public string Name { get; set; }
}
public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(g => g.Name)
            .NotEmpty().WithMessage("Genre name is required")
            .MaximumLength(50).WithMessage("Genre name cannot exceed 50 characters");
    }
}