using CURE.Application.DTOs.Auth;
using CURE.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CURE.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var id = await _authService.RegisterAsync(dto);

        return Created("", new
        {
            Id = id
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);

        return Ok(result);
    }
}