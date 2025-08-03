using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SlaPoliciesController : ControllerBase
{
    private readonly ISlaPolicyService _service;
    private readonly ILogger<SlaPoliciesController> _logger;

    public SlaPoliciesController(ISlaPolicyService service, ILogger<SlaPoliciesController> logger)
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
            _logger.LogError(ex, "Error in SlaPoliciesController");
            return StatusCode(500, "Internal server error.");
        }
    }
}