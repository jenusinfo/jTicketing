using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class TicketHistoryService : ITicketHistoryService
{
    private readonly AppDbContext _context;
    private readonly ILogger<TicketHistoryService> _logger;

    public TicketHistoryService(AppDbContext context, ILogger<TicketHistoryService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<TicketHistoryDto>> GetAllAsync()
    {
        try
        {
            return await _context.TicketHistories
                .Select(h => new TicketHistoryDto
                {
                    Id = h.Id,
                    TicketId = h.TicketId,
                    Action = h.Action,
                    PerformedBy = h.PerformedBy,
                    Timestamp = h.Timestamp
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving ticket histories");
            throw;
        }
    }
}
