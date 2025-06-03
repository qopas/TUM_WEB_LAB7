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

        var genres = unitOfWork.Genres
            .Find(g => request.GenreIds.Contains(g.Id));
            
        if (genres.Count() != request.GenreIds.Count())
            return Result<bool>.Failure(["One or more genres not found"]);
        
        await UpdateBookPropertiesAsync(book.Id, request);
        await UpdateBookGenresAsync(book, request.GenreIds);
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

    private async Task UpdateBookGenresAsync(BookRental.Domain.Entities.Book book, IEnumerable<string> genreIds)
    {
        var currentGenreIds = book.BookGenres.Select(bg => bg.GenreId);
        
        var genresToRemove = book.BookGenres.Where(bg => !genreIds.Contains(bg.GenreId));
        var genreIdsToAdd = genreIds.Except(currentGenreIds);
        
        foreach (var bookGenre in genresToRemove)
        {
            await unitOfWork.BookGenre.SoftDeleteAsync(bookGenre.Id); 
        }
        
        foreach (var genreId in genreIdsToAdd)
        {
            var newBookGenre = BookRental.Domain.Entities.BookGenre.Create(book.Id, genreId);
            await unitOfWork.BookGenre.CreateAsync(newBookGenre);
        }
    }
}