using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

// Add this using directive to ensure the correct ClaimsPrincipal type is used
using ClaimsPrincipal = System.Security.Claims.ClaimsPrincipal;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;
    private readonly ILogger<NotificationsController> _logger;

    public NotificationsController(INotificationService notificationService, ILogger<NotificationsController> logger)
    {
        _notificationService = notificationService;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            // Remove explicit cast and call the method with correct return type
            var notifications = await _notificationService.GetAllAsync(User);
            return Ok(notifications);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in NotificationsController.GetAll");
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPost("mark-read")]
    [Authorize]
    public async Task<IActionResult> MarkAsRead([FromBody] long id)
    {
        try
        {
            var result = await _notificationService.MarkAsReadAsync(id, User);
            return result ? Ok() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in NotificationsController.MarkAsRead");
            return StatusCode(500, "Internal server error.");
        }
    }
}
