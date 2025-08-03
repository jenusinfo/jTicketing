public class TicketHistoryService : ITicketHistoryService
{
    public Task<IEnumerable<TicketHistoryDto>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<TicketHistoryDto>>(new List<TicketHistoryDto>());
    }
}