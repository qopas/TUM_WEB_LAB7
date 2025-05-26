using Microsoft.AspNetCore.Mvc;
using Application.Book.Commands.CreateBook;
using Application.Book.Commands.DeleteBook;
using Application.Book.Commands.UpdateBook;
using Application.Book.Queries.GetBookById;
using Application.Book.Queries.GetBooks;
using BookRental.Web.Models;
using MediatR;

namespace BookRental.Web.Controllers;

public class BookController(IMediator mediator) : BaseWebController
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return await ExecuteAsync(async () =>
        {
            var books = await mediator.Send(new GetBooksQuery());
            return books.Select(BookViewModel.FromDto);
        });
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        return await ExecuteAsync(async () =>
        {
            var book = await mediator.Send(new GetBookByIdQuery { Id = id });
            return BookViewModel.FromDto(book);
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(BookViewModel model)
    {
        return await ExecuteJsonAsync(async () =>
            await mediator.Send(model.ToCreateCommand())
        );
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(BookViewModel model)
    {
        return await ExecuteJsonAsync(async () =>
            await mediator.Send(model.ToUpdateCommand())
        );
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        return await ExecuteJsonAsync(async () =>
            await mediator.Send(new DeleteBookCommand { Id = id })
        );
    }
}