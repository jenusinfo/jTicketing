using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserSessionsController : ControllerBase
{
    private readonly IUserSessionService _service;
    private readonly ILogger<UserSessionsController> _logger;

    public UserSessionsController(IUserSessionService service, ILogger<UserSessionsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UserSessionsController");
            return StatusCode(500, "Internal server error.");
        }
    }
}