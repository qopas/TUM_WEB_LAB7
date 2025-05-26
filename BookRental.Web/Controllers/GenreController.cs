using Microsoft.AspNetCore.Mvc;
using Application.Genres.Queries.GetGenres;
using MediatR;

namespace BookRental.Web.Controllers;

public class GenreController(IMediator mediator) : BaseWebController
{
    [HttpGet]
    public async Task<IActionResult> GetGenresNames()
    {
        return await ExecuteJsonAsync(async () =>
        {
            var genres = await mediator.Send(new GetGenresQuery());
            return genres.Select(g => new { Id = g.Id, Name = g.Name }).ToList();
        });
    }
}