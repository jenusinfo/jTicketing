public interface IReportService
{
    Task<DashboardStatsDto> GetDashboardStatsAsync();
}