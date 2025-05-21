using Application.DTOs.Book;
using Application.Mapping;
using Application.Mediator.Book.Commands.CreateBook;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Book.Commands.CreateBook;

public class CreateBookCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateBookCommand, BookDto>
{
    public async Task<BookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new BookRental.Domain.Entities.Book
        {
            Title = request.Title,
            Author = request.Author,
            PublicationDate = request.PublicationDate,
            GenreId = request.GenreId,
            AvailableQuantity = request.AvailableQuantity,
            RentalPrice = request.RentalPrice
        };
        
        var createdBook = await unitOfWork.Books.AddAsync(book);
        await unitOfWork.SaveChangesAsync();
        return BookDto.FromEntity(createdBook);
    }
}