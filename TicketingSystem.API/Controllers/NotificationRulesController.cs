public class NotificationRulesController : ControllerBase
{
    private readonly INotificationRuleService _service;
    private readonly ILogger<NotificationRulesController> _logger;

    public NotificationRulesController(INotificationRuleService service, ILogger<NotificationRulesController> logger)
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
            _logger.LogError(ex, "Error in NotificationRulesController");
            return StatusCode(500, "Internal server error.");
        }
    }
}