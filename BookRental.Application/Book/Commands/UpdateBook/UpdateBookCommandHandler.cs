using BookRental.Domain.Interfaces;
using BookRental.Domain.Common;
using BookRental.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Book.Commands.UpdateBook;

public class UpdateBookCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateBookCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await unitOfWork.Books.GetByIdOrThrowAsync(request.Id);
        if (book == null)
            return Result<bool>.Failure(["Book not found"]);

        var genreValidation = await ValidateGenresAsync(request.GenreIds, cancellationToken);
        if (!genreValidation.IsSuccess)
            return Result<bool>.Failure(genreValidation.Errors);
        
        await UpdateBookPropertiesAsync(book.Id, request);
        await UpdateBookGenresAsync(book, request.GenreIds);
        await unitOfWork.SaveChangesAsync();
        
        return Result<bool>.Success(true);
    }

    private async Task<Result<bool>> ValidateGenresAsync(IEnumerable<string> genreIds, CancellationToken cancellationToken)
    {
        if (!genreIds.Any()) return Result<bool>.Success(true);
        
        var existingCount = await unitOfWork.Genres
            .Find(g => genreIds.Contains(g.Id))
            .CountAsync(cancellationToken);
            
        return existingCount == genreIds.Count() 
            ? Result<bool>.Success(true) 
            : Result<bool>.Failure(["One or more genres not found"]);
    }

    private async Task UpdateBookPropertiesAsync(string bookId, UpdateBookCommand request)
    {
        await unitOfWork.Books.UpdateAsync(bookId, setters => setters
            .SetProperty(b => b.Title, request.Title)
            .SetProperty(b => b.Author, request.Author)
            .SetProperty(b => b.PublicationDate, request.PublicationDate)
            .SetProperty(b => b.AvailableQuantity, request.AvailableQuantity)
            .SetProperty(b => b.RentalPrice, request.RentalPrice));
    }

    private async Task UpdateBookGenresAsync(BookRental.Domain.Entities.Book book, IEnumerable<string> genreIds)
    {
        book.Genres.Clear();
        
        if (!genreIds.Any()) return;
        
        var genresQuery = unitOfWork.Genres.Find(g => genreIds.Contains(g.Id));
        
        await foreach (var genre in genresQuery.AsAsyncEnumerable())
        {
            book.Genres.Add(genre);
        }
    }
}