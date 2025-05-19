using Application.DTOs.Genre;
using Application.Mapping;
using Application.Mediator.Genres.Commands.CreateGenre;
using Application.Mediator.Genres.Commands.DeleteGenre;
using Application.Mediator.Genres.Commands.UpdateGenre;
using Application.Mediator.Genres.Queries.GetGenreById;
using Application.Mediator.Genres.Queries.GetGenres;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenreController(IMediator mediator)  : ControllerBase
{

    [HttpGet]
    [ProducesResponseType(typeof(List<GenreDto>), StatusCodes.Status200OK)]
     public async Task<IActionResult> GetGenres()
    {
        var result = await mediator.Send( new GetGenresQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGenre(string id)
    {
        var result = await mediator.Send(new GetGenreByIdQuery { Id = id });
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateGenre([FromBody] CreateGenreCommand createGenre)
    {
        var result = await mediator.Send(createGenre);
        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateGenre([FromBody] UpdateGenreCommand updateGenre)
    {
        var result = await mediator.Send(updateGenre);
        
        if (!result)
        {
            return NotFound();
        }
        
        return Ok($"Genre with id: {updateGenre.Id} was successfully updated");
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteGenre(string id)
    {
        var result = await mediator.Send(new DeleteGenreCommand { Id = id });
        if (!result)
        {
            return NotFound();
        }
        return Ok($"Genre with id: {id} deleted");
    }
}