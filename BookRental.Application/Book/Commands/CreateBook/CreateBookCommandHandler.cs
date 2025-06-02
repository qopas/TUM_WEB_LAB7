using Application.DTOs.Book;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Common;
using BookRental.Domain.Entities.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Book.Commands.CreateBook;

public class CreateBookCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateBookCommand, Result<BookDto>>
{
    public async Task<Result<BookDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var genres =  unitOfWork.Genres
            .Find(g => request.GenreIds.Contains(g.Id));
            
        if (genres.Count() != request.GenreIds.Count())
            return Result<BookDto>.Failure(["One or more genres not found"]);

        var bookResult = BookRental.Domain.Entities.Book.Create(new BookModel
        {
            Title = request.Title,
            Author = request.Author,
            PublicationDate = request.PublicationDate,
            AvailableQuantity = request.AvailableQuantity,
            RentalPrice = request.RentalPrice,
            GenreIds = request.GenreIds
        });
        
        if (!bookResult.IsSuccess)
            return Result<BookDto>.Failure(bookResult.Errors);

        var createdBook = await unitOfWork.Books.CreateAsync(bookResult.Value);
        await AssignGenres(createdBook.Id, genres);
        await unitOfWork.SaveChangesAsync();
        
        return Result<BookDto>.Success(BookDto.FromEntity(createdBook));
    }

    private async Task AssignGenres(string bookId, IEnumerable<BookRental.Domain.Entities.Genre> genres)
    {
        foreach (var genre in genres)
        {
            await unitOfWork.BookGenre.CreateAsync(BookRental.Domain.Entities.BookGenre.Create(bookId, genre.Id));
        }
    }
}