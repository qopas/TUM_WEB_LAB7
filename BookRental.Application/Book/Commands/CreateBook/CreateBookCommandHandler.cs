using Application.DTOs.Book;
using BookRental.Domain.Interfaces;
using MediatR;

namespace Application.Book.Commands.CreateBook;

public class CreateBookCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateBookCommand, BookDto>
{
    public async Task<BookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = Convert(request);
        
        var createdBook = await unitOfWork.Books.AddAsync(book);
        await unitOfWork.SaveChangesAsync();
        return BookDto.FromEntity(createdBook);
    }

    private static BookRental.Domain.Entities.Book Convert(CreateBookCommand request)
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
        return book;
    }
}