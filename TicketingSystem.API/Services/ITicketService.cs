public interface ITicketService
{
    Task<IEnumerable<object>> GetTicketsAsync(ClaimsPrincipal user);
    Task<object> CreateTicketAsync(TicketCreateDto dto, ClaimsPrincipal user);
    Task<object> GetTicketByIdAsync(int id, ClaimsPrincipal user);
    Task<object> AssignTicketAsync(int ticketId, ClaimsPrincipal user);
    Task<object> ChangeTicketStatusAsync(int ticketId, string newStatus, ClaimsPrincipal user);
}
