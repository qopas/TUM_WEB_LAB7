using Application.DTOs.Genre;
using Application.Genres.Commands.CreateGenre;
using Application.Genres.Commands.DeleteGenre;
using Application.Genres.Commands.UpdateGenre;
using Application.Genres.Queries.GetGenreById;
using Application.Genres.Queries.GetGenres;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenreController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<GenreDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGenres()
    {
        try
        {
            var result = await mediator.Send(new GetGenresQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGenre(string id)
    {
        try
        {
            var result = await mediator.Send(new GetGenreByIdQuery { Id = id });
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateGenre([FromBody] CreateGenreCommand createGenre)
    {
        try
        {
            var result = await mediator.Send(createGenre);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateGenre([FromBody] UpdateGenreCommand updateGenre)
    {
        try
        {
            var result = await mediator.Send(updateGenre);
            return Ok($"Genre with id: {updateGenre.Id} was successfully updated");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteGenre(string id)
    {
        try
        {
            var result = await mediator.Send(new DeleteGenreCommand { Id = id });
            return Ok($"Genre with id: {id} deleted");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
}