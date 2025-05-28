using Application.DTOs.Destination;
using BookRental.Controllers;
using BookRental.DTOs.In.Destination;
using BookRental.DTOs.Out;
using BookRental.DTOs.Out.Destination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[SwaggerTag("Manage rental destinations")]
public class DestinationController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all destinations")]
    [ProducesResponseType(typeof(BaseEnumerableResponse<DestinationResponse, DestinationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDestinations()
    {
        return await
            ExecuteAsync<BaseEnumerableResponse<DestinationResponse, DestinationDto>, IEnumerable<DestinationDto>>(
                new GetDestinationsRequest().Convert());
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get destination by ID")]
    [ProducesResponseType(typeof(BaseResponse<DestinationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDestination(string id)
    {
        return await ExecuteAsync<DestinationResponse, DestinationDto>(
            new GetDestinationByIdRequest { Id = id }.Convert());
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create new destination")]
    [ProducesResponseType(typeof(BaseResponse<DestinationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateDestination([FromBody] CreateDestinationRequest request)
    {
        return await ExecuteAsync<DestinationResponse, DestinationDto>(request.Convert());
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update destination")]
    [ProducesResponseType(typeof(BaseResponse<DestinationUpdateResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateDestination([FromBody] UpdateDestinationRequest request)
    {
        return await ExecuteAsync<DestinationUpdateResponse, bool>(request.Convert());
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete destination")]
    [ProducesResponseType(typeof(BaseResponse<DestinationDeleteResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteDestination(string id)
    {
        return await ExecuteAsync<DestinationDeleteResponse, bool>(new DeleteDestinationRequest { Id = id }.Convert());
    }
}