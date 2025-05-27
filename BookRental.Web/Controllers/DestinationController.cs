using Application.Destination.Commands.DeleteDestination;
using Application.Destination.Queries.GetDestinationById;
using Microsoft.AspNetCore.Mvc;
using Application.Destination.Queries.GetDestinations;
using BookRental.Web.Models;
using MediatR;

namespace BookRental.Web.Controllers;

public class DestinationController(IMediator mediator) : BaseWebController
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return await ExecuteViewAsync(async () =>
        {
            var destinations = await mediator.Send(new GetDestinationsQuery());
            return destinations.Select(DestinationViewModel.FromDto);
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetDestinationsData()
    {
        return await ExecuteAsync(async () =>
        {
            var destinations = await mediator.Send(new GetDestinationsQuery());
            return new { 
                data = destinations.Select(DestinationViewModel.FromDto)
            };
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetModalBody(string? id = null)
    {
        if (string.IsNullOrEmpty(id))
        {
            return PartialView("_DestinationModalBody", new DestinationViewModel());
        }
        else
        {
            var destination = await mediator.Send(new GetDestinationByIdQuery { Id = id });
            var viewModel = DestinationViewModel.FromDto(destination);
            return PartialView("_DestinationModalBody", viewModel);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        return await ExecuteAsync(async () =>
        {
            var destination = await mediator.Send(new GetDestinationByIdQuery { Id = id });
            return DestinationViewModel.FromDto(destination);
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(DestinationViewModel model)
    {
        return await ExecuteAsync(async () =>
            await mediator.Send(model.ToCreateCommand())
        );
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(DestinationViewModel model)
    {
        return await ExecuteAsync(async () =>
            await mediator.Send(model.ToUpdateCommand())
        );
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        return await ExecuteAsync(async () =>
            await mediator.Send(new DeleteDestinationCommand { Id = id })
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetDestinationsNames()
    {
        return await ExecuteAsync(async () =>
        {
            var destinations = await mediator.Send(new GetDestinationsQuery());
            return destinations.Select(d => new { id = d.Id, name = d.Name });
        });
    }
}