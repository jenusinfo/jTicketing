using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class UserSkillsController : ControllerBase
{
    private readonly IUserSkillService _service;
    private readonly ILogger<UserSkillsController> _logger;

    public UserSkillsController(IUserSkillService service, ILogger<UserSkillsController> logger)
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
            _logger.LogError(ex, "Error in UserSkillsController");
            return StatusCode(500, "Internal server error.");
        }
    }
}