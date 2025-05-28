using Application.Authentication.Commands.Login;
using Application.Authentication.Commands.Logout;
using Application.Authentication.Commands.RefreshToken;
using Application.Authentication.Commands.Register;
using Application.DTOs.Authentication;
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
    [SwaggerOperation(Summary = "Register new user", Description = "Create a new user account in the system")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        return await ExecuteAsync(command);
    }

    [AllowAnonymous]
    [HttpPost(nameof(Login))]
    [SwaggerOperation(Summary = "User login", Description = "Authenticate user and return access token")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        return await ExecuteAsync(command);
    }

    [HttpPost(nameof(RefreshToken))]
    [SwaggerOperation(Summary = "Refresh access token", Description = "Generate new access token using refresh token")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        return await ExecuteAsync(command);
    }

    [HttpPost(nameof(Logout))]
    [SwaggerOperation(Summary = "User logout", Description = "Logout user and invalidate current session")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout([FromBody] LogoutCommand command)
    {
        return await ExecuteAsync(command);
    }

    [HttpPost(nameof(LogoutAll))]
    [Authorize]
    [SwaggerOperation(Summary = "Logout from all devices", Description = "Logout user from all devices and invalidate all sessions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> LogoutAll(LogoutCommand command)
    {
        return await ExecuteAsync(command);
    }
}