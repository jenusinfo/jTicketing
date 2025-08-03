using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest dto)
    {
        // Explicitly call the string-returning overload to resolve ambiguity
        return Ok(await ((IAuthService)_authService).RegisterAsync(dto));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest dto) => Ok(await _authService.LoginAsync(dto));
}