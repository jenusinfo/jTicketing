using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TicketHistoriesController : ControllerBase
{
    private readonly ITicketHistoryService _service;
    private readonly ILogger<TicketHistoriesController> _logger;

    public TicketHistoriesController(ITicketHistoryService service, ILogger<TicketHistoriesController> logger)
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
            _logger.LogError(ex, "Error in TicketHistoriesController");
            return StatusCode(500, "Internal server error.");
        }
    }
}