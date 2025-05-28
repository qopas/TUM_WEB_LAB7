using Application.DTOs.Genre;
using Application.Genres.Commands.CreateGenre;
using Application.Genres.Commands.DeleteGenre;
using Application.Genres.Commands.UpdateGenre;
using Application.Genres.Queries.GetGenreById;
using Application.Genres.Queries.GetGenres;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookRental.Controllers;

[SwaggerTag("Manage book genres")]
public class GenreController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all genres", Description = "Retrieve a list of all book genres available in the system")]
    [ProducesResponseType(typeof(List<GenreDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGenres()
    {
        return await ExecuteAsync(new GetGenresQuery());
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get genre by ID", Description = "Retrieve details of a specific genre using its unique identifier")]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGenre(string id)
    {
        return await ExecuteAsync(new GetGenreByIdQuery { Id = id });
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create new genre", Description = "Add a new book genre to the system")]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateGenre([FromBody] CreateGenreCommand createGenre)
    {
        return await ExecuteAsync(createGenre);
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update genre", Description = "Update details of an existing genre")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateGenre([FromBody] UpdateGenreCommand updateGenre)
    {
        return await ExecuteAsync(updateGenre);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete genre", Description = "Remove a genre from the system using its ID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteGenre(string id)
    {
        return await ExecuteAsync(new DeleteGenreCommand { Id = id });
    }
}