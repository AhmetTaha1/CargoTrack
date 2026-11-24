
using CargoTrack.Application.DTOs;
using CargoTrack.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CargoTrack.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService=authService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var user=await _authService.RegisterAsync(request);
        return StatusCode(StatusCodes.Status201Created,user);
    }

}