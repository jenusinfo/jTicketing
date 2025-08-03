using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; // <-- Add this using directive

public class OrganizationsController : ControllerBase
{
    private readonly IOrganizationService _service;
    private readonly ILogger<OrganizationsController> _logger;

    public OrganizationsController(IOrganizationService service, ILogger<OrganizationsController> logger)
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
            _logger.LogError(ex, "Error in OrganizationsController");
            return StatusCode(500, "Internal server error.");
        }
    }
}