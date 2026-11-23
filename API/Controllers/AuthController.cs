using Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(IUserService userService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(AuthenticationRequest request)
    {
        var result = await userService.RegisterAsync(request.UserName, request.Password);
        return result.Succeeded ? NoContent() : BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthenticationRequest request)
    {
        var result = await userService.LoginAsync(request.UserName, request.Password);
        return result.Succeeded ? NoContent() : Unauthorized(result.Errors);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await userService.LogoutAsync();
        return NoContent();
    }
}

public sealed record AuthenticationRequest(string UserName, string Password);