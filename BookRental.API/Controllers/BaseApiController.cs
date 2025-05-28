  using Application.DTOs.Authentication;
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
      protected async Task<IActionResult> ExecuteAsync(IBaseRequest request)
      {
          try
          {
              var result = await mediator.Send(request);
              return Ok(result);
          }
          catch (Exception ex)
          {
              return BadRequest(new { success = false, error = ex.Message });
          }
      }
      protected async Task<IActionResult> ExecuteAsync<TOutResponse, TResult>(IBaseRequest request)
          where TOutResponse : IResponseOut<TResult>, new()
      {
          try
          {
              var result = await mediator.Send(request);
              if (result == null) return Ok();

              var responseInstance = new TOutResponse();
              var response = responseInstance.Convert((TResult)result);
              return Ok(response);
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
      }
  }