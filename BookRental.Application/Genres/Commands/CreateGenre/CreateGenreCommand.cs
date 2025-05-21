using Application.DTOs.Genre;
using FluentValidation;
using MediatR;

namespace Application.Genres.Commands.CreateGenre;

public class CreateGenreCommand : IRequest<GenreDto>
{
    public required string Name { get; init; }
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