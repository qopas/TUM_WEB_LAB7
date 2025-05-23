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
public class GenreController(IMediator mediator) : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(List<GenreDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGenres()
    {
        return await ExecuteAsync(async () => await mediator.Send(new GetGenresQuery()));
      
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGenre(string id)
    {
        return await ExecuteAsync(async () => await mediator.Send(new GetGenreByIdQuery { Id = id }));
    }

    [HttpPost]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateGenre([FromBody] CreateGenreCommand createGenre)
    {
        return await ExecuteAsync(async () => await mediator.Send(createGenre));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateGenre([FromBody] UpdateGenreCommand updateGenre)
    {
        return await ExecuteAsync(async () => await mediator.Send(updateGenre));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteGenre(string id)
    {
        return await ExecuteAsync(async () => await mediator.Send(new DeleteGenreCommand { Id = id }));
    }
}