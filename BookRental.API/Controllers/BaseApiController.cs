using BookRental.Domain.Common;
using BookRental.DTOs.Out;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController(IMediator mediator) : ControllerBase
{
    protected async Task<IActionResult> ExecuteAsync<TOutResponse, TResult>(IBaseRequest request)
        where TOutResponse : IResponseOut<TResult>, new()
    {
        var response = new BaseResponse<object>();

        try
        {
            var result = await mediator.Send(request);

            response.Data = result switch
            {
                Result<TResult> { IsSuccess: false } resultObj => throw new Exception(string.Join(", ", resultObj.Errors)),
                Result<TResult> resultObj => new TOutResponse().Convert(resultObj.Value),
                TResult dto => new TOutResponse().Convert(dto),
                _ => result
            };
            response.Success = true;
        }
        catch (Exception ex)
        {
            var errorResponse = new BaseResponse<object>
            {
                Success = false,
                Message = ex.Message,
                Data = null
            };
            return BadRequest(errorResponse);
        }

        return Ok(response);
    }
}