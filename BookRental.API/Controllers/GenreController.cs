using Application.DTOs.Genre;
using BookRental.DTOs.In.Genre;
using BookRental.DTOs.Out;
using BookRental.DTOs.Out.Genre;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookRental.Controllers;

[SwaggerTag("Manage book genres")]
public class GenreController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all genres")]
    [ProducesResponseType(typeof(BaseEnumerableResponse<GenreResponse, GenreDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGenres()
    {
        return await ExecuteAsync<BaseEnumerableResponse<GenreResponse, GenreDto>, IEnumerable<GenreDto>>(
            new GetGenresRequest().Convert());
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get genre by ID")]
    [ProducesResponseType(typeof(BaseResponse<GenreResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGenre(string id)
    {
        return await ExecuteAsync<GenreResponse, GenreDto>(new GetGenreByIdRequest { Id = id }.Convert());
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create new genre")]
    [ProducesResponseType(typeof(BaseResponse<GenreDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateGenre([FromBody] CreateGenreRequest request)
    {
        return await ExecuteAsync<GenreResponse, GenreDto>(request.Convert());
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update genre")]
    [ProducesResponseType(typeof(BaseResponse<GenreUpdateResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateGenre([FromBody] UpdateGenreRequest request)
    {
        return await ExecuteAsync<GenreUpdateResponse, bool>(request.Convert());
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete genre")]
    [ProducesResponseType(typeof(BaseResponse<GenreDeleteResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteGenre(string id)
    {
        return await ExecuteAsync<GenreDeleteResponse, bool>(new DeleteGenreRequest { Id = id }.Convert());
    }
}