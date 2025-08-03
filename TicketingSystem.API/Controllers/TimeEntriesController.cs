using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TimeEntriesController : ControllerBase
{
    private readonly ITimeEntryService _service;
    private readonly ILogger<TimeEntriesController> _logger;

    public TimeEntriesController(ITimeEntryService service, ILogger<TimeEntriesController> logger)
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
            _logger.LogError(ex, "Error in TimeEntriesController");
            return StatusCode(500, "Internal server error.");
        }
    }
}