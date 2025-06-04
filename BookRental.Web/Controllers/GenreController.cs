using Application.Genres.Commands.DeleteGenre;
using Application.Genres.Queries.GetGenreById;
using Microsoft.AspNetCore.Mvc;
using Application.Genres.Queries.GetGenres;
using BookRental.Web.Models;
using MediatR;

namespace BookRental.Web.Controllers;

public class GenreController(IMediator mediator) : BaseWebController
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return await ExecuteViewAsync(async () =>
        {
            var genres = await mediator.Send(new GetGenresQuery());
            return genres.Select(GenreViewModel.FromDto);
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetGenresData()
    {
        return await ExecuteAsync(async () =>
        {
            var genres = await mediator.Send(new GetGenresQuery());
            return new { 
                data = genres.Select(GenreViewModel.FromDto)
            };
        });
    }

    [HttpPost]
    public IActionResult GetModalBody([FromForm] GenreViewModel? model = null)
    {
        var viewModel = model ?? new GenreViewModel();
        return PartialView("_GenreModalBody", viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        return await ExecuteAsync(async () =>
        {
            var genre = await mediator.Send(new GetGenreByIdQuery { Id = id });
            return GenreViewModel.FromDto(genre);
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(GenreViewModel model)
    {
        return await ExecuteAsync(async () =>
            await mediator.Send(model.ToCreateCommand())
        );
    }
    
    [HttpGet]
    public async Task<IActionResult> GetGenresNames()
    {
        return await ExecuteAsync(async () =>
        {
            var genres = await mediator.Send(new GetGenresQuery());
            return genres.Select(g => new { id = g.Id, name = g.Name });
        });
    }
}