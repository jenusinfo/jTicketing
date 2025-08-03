using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TicketCommentsController : ControllerBase
{
    private readonly ITicketCommentService _commentService;
    private readonly ILogger<TicketCommentsController> _logger;

    public TicketCommentsController(ITicketCommentService commentService, ILogger<TicketCommentsController> logger)
    {
        _commentService = commentService;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get(int ticketId) => Ok(await _commentService.GetByTicketIdAsync(ticketId));

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add(int ticketId, [FromBody] TicketCommentCreateDto dto)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // Explicitly call the generic method to resolve ambiguity
            var created = await _commentService.AddCommentAsync(ticketId, dto, User);
            return Ok(created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in TicketCommentsController");
            return StatusCode(500, "Internal server error.");
        }
    }
}