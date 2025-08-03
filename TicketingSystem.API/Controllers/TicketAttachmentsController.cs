public class TicketAttachmentsController : ControllerBase
{
    private readonly ITicketAttachmentService _service;
    private readonly ILogger<TicketAttachmentsController> _logger;

    public TicketAttachmentsController(ITicketAttachmentService service, ILogger<TicketAttachmentsController> logger)
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
            _logger.LogError(ex, "Error in TicketAttachmentsController");
            return StatusCode(500, "Internal server error.");
        }
    }
}