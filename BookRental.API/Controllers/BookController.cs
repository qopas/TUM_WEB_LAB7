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
public class BookController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBooks()
    {
        try
        {
            var query = new GetBooksQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBook(string id)
    {
        try
        {
            var query = new GetBookByIdQuery { Id = id };
            var result = await mediator.Send(query);
            
            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command)
    {
        try
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateBook([FromBody] UpdateBookCommand command)
    {
        try
        {
            var result = await mediator.Send(command);
            
            if (!result)
            {
                return NotFound();
            }
            
            return Ok($"Book with id: {command.Id} was successfully updated");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteBook(string id)
    {
        try
        {
            var command = new DeleteBookCommand { Id = id };
            var result = await mediator.Send(command);
            
            if (!result)
            {
                return NotFound();
            }
            
            return Ok($"Book with id: {id} deleted");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
}