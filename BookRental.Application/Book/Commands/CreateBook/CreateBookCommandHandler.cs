using Application.DTOs.Book;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Common;
using BookRental.Domain.Entities.Models;
using MediatR;

namespace Application.Book.Commands.CreateBook;

public class CreateBookCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateBookCommand, Result<BookDto>>
{
    public async Task<Result<BookDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var bookModel = new BookModel
        {
            Title = request.Title,
            Author = request.Author,
            PublicationDate = request.PublicationDate,
            GenreId = request.GenreId,
            AvailableQuantity = request.AvailableQuantity,
            RentalPrice = request.RentalPrice
        };
        
        var bookResult = BookRental.Domain.Entities.Book.Create(bookModel);
        if (!bookResult.IsSuccess)
            return Result<BookDto>.Failure(bookResult.Errors);

        var createdBook = await unitOfWork.Books.CreateAsync(bookResult.Value);
        await unitOfWork.SaveChangesAsync();
        return Result<BookDto>.Success(BookDto.FromEntity(createdBook));
    }
}
