using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;
    private readonly ILogger<ReportsController> _logger;

    public ReportsController(IReportService reportService, ILogger<ReportsController> logger)
    {
        _reportService = reportService;
        _logger = logger;
    }

    [HttpGet("dashboard")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetDashboardStats() => Ok(await _reportService.GetDashboardStatsAsync());
}