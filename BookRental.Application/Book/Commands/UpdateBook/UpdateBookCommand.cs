using BookRental.Domain.Common;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Book.Commands.UpdateBook;

public class UpdateBookCommand : IRequest<Result<bool>>
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public required string Author { get; init; }
    public required DateTimeOffset PublicationDate { get; init; }
    public required IEnumerable<string> GenreIds { get; init; }
    public required int AvailableQuantity { get; init; }
    public required decimal RentalPrice { get; init; }
}
public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator(IStringLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(localizer["bookIdRequired"]);

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage(localizer["titleRequired"])
            .MaximumLength(200).WithMessage(localizer["titleMaxLength"]);

        RuleFor(x => x.Author)
            .NotEmpty().WithMessage(localizer["authorRequired"])
            .MaximumLength(100).WithMessage(localizer["authorMaxLength"]);

        RuleFor(x => x.PublicationDate)
            .NotEmpty().WithMessage(localizer["publicationDateRequired"])
            .LessThanOrEqualTo(DateTimeOffset.Now).WithMessage(localizer["publicationDateFuture"]);

        RuleFor(x => x.GenreIds)
            .NotEmpty().WithMessage(localizer["genreRequired"])
            .Must(ids => ids.All(id => Guid.TryParse(id, out _)))
            .WithMessage(localizer["invalidGenreIds"]);

        RuleFor(x => x.AvailableQuantity)
            .GreaterThanOrEqualTo(0).WithMessage(localizer["quantityNegative"]);

        RuleFor(x => x.RentalPrice)
            .GreaterThan(0).WithMessage(localizer["pricePositive"]);
    }
}