using Application.DTOs.Book;
using Application.Mediator.Book.Commands.CreateBook;
using Application.Mediator.Book.Commands.DeleteBook;
using Application.Mediator.Book.Commands.UpdateBook;
using Application.Mediator.Book.Queries.GetBookById;
using Application.Mediator.Book.Queries.GetBooks;
using Application.Validators.Book;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BookController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBooks()
    {
        var query = new GetBooksQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBook(string id)
    {
        var query = new GetBookByIdQuery { Id = id };
        var result = await mediator.Send(query);
        
        if (result == null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command)
    {
        
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateBook([FromBody] UpdateBookCommand command)
    {
        var result = await mediator.Send(command);
        
        if (!result)
        {
            return NotFound();
        }
        
        return Ok($"Book with id: {command.Id} was successfully updated");
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteBook(string id)
    {
        var command = new DeleteBookCommand { Id = id };
        var result = await mediator.Send(command);
        
        if (!result)
        {
            return NotFound();
        }
        
        return Ok($"Book with id: {id} deleted");
    }
}