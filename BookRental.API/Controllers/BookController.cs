using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BookController: ControllerBase
{
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        var books = _bookRepository.GetAllAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = _bookRepository.GetByIdAsync(id);
        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<Book>> AddBook([FromBody] Book book)
    {
        await _bookRepository.AddAsync(book);
        return Ok(book);
    }
}