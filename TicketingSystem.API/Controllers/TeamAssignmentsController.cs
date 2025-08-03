using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; // <-- Add this using directive
public class TeamAssignmentsController : ControllerBase
{
    private readonly ITeamAssignmentService _service;
    private readonly ILogger<TeamAssignmentsController> _logger;

    public TeamAssignmentsController(ITeamAssignmentService service, ILogger<TeamAssignmentsController> logger)
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
            _logger.LogError(ex, "Error in TeamAssignmentsController");
            return StatusCode(500, "Internal server error.");
        }
    }
}