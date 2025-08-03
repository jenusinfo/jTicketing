using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class NotificationQueuesController : ControllerBase
{
    private readonly INotificationQueueService _service;
    private readonly ILogger<NotificationQueuesController> _logger;

    public NotificationQueuesController(INotificationQueueService service, ILogger<NotificationQueuesController> logger)
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
            _logger.LogError(ex, "Error in NotificationQueuesController");
            return StatusCode(500, "Internal server error.");
        }
    }
}