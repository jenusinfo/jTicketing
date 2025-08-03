public interface ITicketHistoryService
{
    Task<IEnumerable<TicketHistoryDto>> GetAllAsync();
}