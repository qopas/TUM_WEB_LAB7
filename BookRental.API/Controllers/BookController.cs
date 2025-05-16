using Application.Mapping;
using BookRental.Domain.DTOs.Book;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BookController(IBookRepository bookRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
    {
        var books = await bookRepository.GetAllAsync();
        return Ok(books.ToDtoList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(string id)
    {
        var book = await bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book.ToDto());
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookDto createBook)
    {
        if (createBook == null)
        {
            return BadRequest();
        }
        var createdBook = await bookRepository.AddAsync(createBook.ToEntity());
        return Ok(createdBook);
    }

    [HttpPut]
    public async Task<ActionResult<Book>> UpdateBook([FromBody] UpdateBookDto updateBook)
    {
        var existingBook = await bookRepository.GetByIdAsync(updateBook.Id);
        if (existingBook == null)
        {
            return NotFound();
        }
        await bookRepository.UpdateAsync(updateBook.ToEntity(existingBook));
        return Ok($"Book with id: {updateBook.Id} was successfully updated");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Book>> DeleteBook(string id)
    { 
        var book = await bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        await bookRepository.DeleteAsync(id);
        return Ok($"Book with id: {id} deleted");
    }
}