using System.Reflection;
using Application.DTOs.Book;
using Application.Book.Queries.GetBooks;
using Application.Book.Queries.GetBookById;
using Application.Book.Commands.CreateBook;
using Application.Book.Commands.UpdateBook;
using Application.Book.Commands.DeleteBook;
using BookRental.DTOs.In.Book;
using BookRental.DTOs.Out;
using BookRental.DTOs.Out.Book;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookRental.Controllers;

[AllowAnonymous]
[SwaggerTag("Manage books in the rental system")]
public class BookController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all books")]
    [ProducesResponseType(typeof(BaseEnumerableResponse<BookDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBooks()
    {
        var request = new GetBooksQuery();
        return await ExecuteAsync<BaseEnumerableResponse<BookDto>, IEnumerable<BookDto>>(request);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get book by ID")]
    [ProducesResponseType(typeof(BaseResponse<BookDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBook(string id)
    {
        var request = new GetBookByIdQuery { Id = id };
        return await ExecuteAsync<BaseResponse<BookDto>, BookDto>(request);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create new book")]
    [ProducesResponseType(typeof(BaseResponse<BookDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookInRequest inRequest)
    {
        var request = inRequest.Convert();
        return await ExecuteAsync<BaseResponse<BookDto>, BookDto>(request);
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update book")]
    [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateBook([FromBody] UpdateBookInRequest inRequest)
    {
        var request = inRequest.Convert();
        return await ExecuteAsync<BaseResponse<bool>, bool>(request);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete book")]
    [ProducesResponseType(typeof(BaseResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteBook(string id)
    {
        var request = new DeleteBookInRequest { Id = id };
        var command = request.Convert();
        return await ExecuteAsync<BaseResponse<bool>, bool>(command);
    }
}