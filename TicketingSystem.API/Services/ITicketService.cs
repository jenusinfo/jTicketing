using System.Security.Claims;

public interface ITicketService
{
    Task<IEnumerable<TicketDto>> GetAllAsync(ClaimsPrincipal user);
    Task<TicketDto> GetByIdAsync(int id, ClaimsPrincipal user);
    Task<TicketDto> CreateAsync(TicketCreateDto dto, ClaimsPrincipal user);
   
    Task<bool> AssignToAgentAsync(int ticketId, int agentId);
}