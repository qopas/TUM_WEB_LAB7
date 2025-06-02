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
        var genreValidation = await ValidateGenresAsync(request.GenreIds, cancellationToken);
        if (!genreValidation.IsSuccess)
            return Result<BookDto>.Failure(genreValidation.Errors);

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
        await AssignGenresAsync(createdBook, request.GenreIds);
        await unitOfWork.SaveChangesAsync();
        
        return Result<BookDto>.Success(BookDto.FromEntity(createdBook));
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

    private async Task AssignGenresAsync(BookRental.Domain.Entities.Book book, IEnumerable<string> genreIds)
    {
        if (!genreIds.Any()) return;
        
        var genresQuery = unitOfWork.Genres.Find(g => genreIds.Contains(g.Id));
        
        await foreach (var genre in genresQuery.AsAsyncEnumerable())
        {
            book.Genres.Add(genre);
        }
    }
}