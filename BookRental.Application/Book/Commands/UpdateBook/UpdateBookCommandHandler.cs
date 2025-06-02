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

        var genres =  unitOfWork.Genres
            .Find(g => request.GenreIds.Contains(g.Id));
            
        if (genres.Count() != request.GenreIds.Count())
            return Result<bool>.Failure(["One or more genres not found"]);
        
        await UpdateBookPropertiesAsync(book.Id, request);
        UpdateBookGenres(book, genres);
        await unitOfWork.SaveChangesAsync();
        
        return Result<bool>.Success(true);
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

    private static void UpdateBookGenres(BookRental.Domain.Entities.Book book, IEnumerable<BookRental.Domain.Entities.Genre> genres)
    {
        book.Genres.Clear();
        foreach (var genre in genres)
        {
            book.Genres.Add(genre);
        }
    }
}