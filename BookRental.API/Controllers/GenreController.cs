using Application.Mapping;
using BookRental.Domain.DTOs.Genre;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenreController(IRepository<Genre> genreRepository)  : ControllerBase
{

    [HttpGet]
    [ProducesResponseType(typeof(List<GenreDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetGenres()
    {
        var genres = await genreRepository.GetAllAsync();
        return Ok(genres.ToDtoList());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetGenre(string id)
    {
        var genre = await genreRepository.GetByIdAsync(id);
        if (genre == null)
        {
            return NotFound();
        }
        return Ok(genre.ToDto());
    }

    [HttpPost]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateGenre([FromBody] CreateGenreDto createGenre)
    {
        var genre = createGenre.ToEntity();
        var createdGenre = await genreRepository.AddAsync(genre);
        return Ok(createdGenre.ToDto());
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateGenre([FromBody] UpdateGenreDto updateGenre)
    {
        var existingGenre = await genreRepository.GetByIdAsync(updateGenre.Id);
        if (existingGenre == null)
        {
            return NotFound();
        }
        await genreRepository.UpdateAsync(updateGenre.ToEntity(existingGenre));
        return Ok($"Genre with id: {updateGenre.Id} was successfully updated");
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteGenre(string id)
    {
        var genre = await genreRepository.GetByIdAsync(id);
        if (genre == null)
        {
            return NotFound();
        }
        await genreRepository.DeleteAsync(id);
        return Ok($"Genre with id: {id} deleted");
    }
}