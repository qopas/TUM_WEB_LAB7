using Application.Book.Commands.CreateBook;
using Application.Book.Commands.DeleteBook;
using Application.Book.Commands.UpdateBook;
using Application.Book.Queries.GetBookById;
using Application.Book.Queries.GetBooks;
using Application.DTOs.Book;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController(IMediator mediator) : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBooks()
    {
        return await ExecuteAsync(async () => await mediator.Send(new GetBooksQuery()));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBook(string id)
    {
        return await ExecuteAsync(async () => await mediator.Send(new GetBookByIdQuery { Id = id }));
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command)
    {
        return await ExecuteAsync(async () => await mediator.Send(command));
    }
    

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateBook([FromBody] UpdateBookCommand command)
    {
        
        return await ExecuteAsync(async () => await mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteBook(string id)
    {
        return await ExecuteAsync(async () => await mediator.Send(new DeleteBookCommand { Id = id }));
    }
}