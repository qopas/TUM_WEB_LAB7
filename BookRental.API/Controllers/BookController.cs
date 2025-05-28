using Application.Book.Commands.CreateBook;
using Application.Book.Commands.DeleteBook;
using Application.Book.Commands.UpdateBook;
using Application.Book.Queries.GetBookById;
using Application.Book.Queries.GetBooks;
using Application.DTOs.Book;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookRental.Controllers;

[SwaggerTag("Manage books in the rental system")]
public class BookController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all books", Description = "Retrieve a list of all available books in the system")]
    [ProducesResponseType(typeof(IEnumerable<BookDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBooks()
    {
        return await ExecuteAsync(new GetBooksQuery());
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get book by ID", Description = "Retrieve details of a specific book using its unique identifier")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBook(string id)
    {
        return await ExecuteAsync(new GetBookByIdQuery { Id = id });
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create new book", Description = "Add a new book to the rental system")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command)
    {
        return await ExecuteAsync(command);
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update book", Description = "Update details of an existing book")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateBook([FromBody] UpdateBookCommand command)
    {
        return await ExecuteAsync(command);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete book", Description = "Remove a book from the system using its ID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteBook(string id)
    {
        return await ExecuteAsync(new DeleteBookCommand { Id = id });
    }
}