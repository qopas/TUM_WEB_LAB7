using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookRental.Web.Controllers;

[Authorize]
public abstract class BaseWebController : Controller
{
    protected async Task<IActionResult> ExecuteAsync<T>(Func<Task<T>> action)
    {
        var result = await action();
        return Ok(result);
    }

    protected async Task<IActionResult> ExecuteViewAsync<T>(Func<Task<T>> action)
    {
        var result = await action();
        return View(result);
    }

    protected async Task SignInUserAsync(string userId, string token, string email)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId),
            new(ClaimTypes.Email, email),
            new("JwtToken", token)
        };

        var claimsIdentity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = false,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
        };

        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
    }

    protected async Task SignOutUserAsync()
    {
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
    }
}