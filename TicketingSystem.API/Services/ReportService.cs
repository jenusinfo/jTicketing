public class ReportService : IReportService
{
    public Task<DashboardStatsDto> GetDashboardStatsAsync() =>
        Task.FromResult(new DashboardStatsDto { TotalTickets = 100, OpenTickets = 30, ClosedTickets = 70 });
}