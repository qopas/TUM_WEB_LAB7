
using Application.DTOs.Rent;
using BookRental.DTOs.In.Rent;
using BookRental.DTOs.Out;
using BookRental.DTOs.Out.Rent;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookRental.Controllers;

[SwaggerTag("Manage book rental transactions")]
public class RentController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all rentals")]
    [ProducesResponseType(typeof(BaseEnumerableResponse<RentResponse, RentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRents()
    {
        return await ExecuteAsync<BaseEnumerableResponse<RentResponse, RentDto>, IEnumerable<RentDto>>(new GetRentsRequest().Convert());
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get rental by ID")]
    [ProducesResponseType(typeof(BaseResponse<RentResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRent(string id)
    {
        return await ExecuteAsync<RentResponse, RentDto>(new GetRentByIdRequest { Id = id }.Convert());
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create new rental")]
    [ProducesResponseType(typeof(BaseResponse<RentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateRent([FromBody] CreateRentRequest request)
    {
        return await ExecuteAsync<RentResponse, RentDto>(request.Convert());
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update rental")]
    [ProducesResponseType(typeof(BaseResponse<RentUpdateResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateRent([FromBody] UpdateRentRequest request)
    {
        return await ExecuteAsync<RentUpdateResponse, bool>(request.Convert());
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete rental")]
    [ProducesResponseType(typeof(BaseResponse<RentDeleteResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteRent(string id)
    {
        return await ExecuteAsync<RentDeleteResponse, bool>(new DeleteRentRequest { Id = id }.Convert());
    }
}