using Application.DTOs.Authentication;
using BookRental.DTOs.In.Auth;
using BookRental.DTOs.Out;
using BookRental.DTOs.Out.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookRental.Controllers;

[SwaggerTag("Authentication and user management")]
public class AuthController(IMediator mediator) : BaseApiController(mediator)
{
    [AllowAnonymous]
    [HttpPost(nameof(Register))]
    [SwaggerOperation(Summary = "Register new user")]
    [ProducesResponseType(typeof(BaseResponse<AuthResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        return await ExecuteAsync<AuthResponse, AuthResponseDto>(request.Convert());
    }

    [AllowAnonymous]
    [HttpPost(nameof(Login))]
    [SwaggerOperation(Summary = "User login")]
    [ProducesResponseType(typeof(BaseResponse<AuthResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        return await ExecuteAsync<AuthResponse, AuthResponseDto>(request.Convert());
    }

    [HttpPost(nameof(RefreshToken))]
    [SwaggerOperation(Summary = "Refresh access token")]
    [ProducesResponseType(typeof(BaseResponse<AuthResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        return await ExecuteAsync<AuthResponse, AuthResponseDto>(request.Convert());
    }

    [HttpPost(nameof(Logout))]
    [SwaggerOperation(Summary = "User logout")]
    [ProducesResponseType(typeof(BaseResponse<LogoutResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
    {
        return await ExecuteAsync<LogoutResponse, bool>(request.Convert());
    }

    [HttpPost(nameof(LogoutAll))]
    [Authorize]
    [SwaggerOperation(Summary = "Logout from all devices")]
    [ProducesResponseType(typeof(BaseResponse<LogoutResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> LogoutAll([FromBody] LogoutRequest request)
    {
        return await ExecuteAsync<LogoutResponse, bool>(request.Convert());
    }
}