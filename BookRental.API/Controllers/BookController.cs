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

[SwaggerTag("Manage books in the rental system")]
public class BookController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all books")]
    [ProducesResponseType(typeof(BaseEnumerableResponse<BookResponse,BookDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBooks()
    {
        return await ExecuteAsync<BaseEnumerableResponse<BookResponse, BookDto>, IEnumerable<BookDto>>(new GetBooksQuery());
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get book by ID")]
    [ProducesResponseType(typeof(BaseResponse<BookResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBook(string id)
    {
        return await ExecuteAsync<BookResponse, BookDto>(new GetBookByIdQuery { Id = id });
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create new book")]
    [ProducesResponseType(typeof(BaseResponse<BookDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest request)
    {
        return await ExecuteAsync<BookResponse, BookDto>(request.Convert());
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update book")]
    [ProducesResponseType(typeof(BaseResponse<BookUpdateResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateBook([FromBody] UpdateBookRequest request)
    {
        return await ExecuteAsync<BookUpdateResponse, bool>(request.Convert());
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete book")]
    [ProducesResponseType(typeof(BaseResponse<BookDeleteResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteBook(string id)
    {
        return await ExecuteAsync<BookDeleteResponse, bool>(new DeleteBookRequest { Id = id }.Convert());
    }
}