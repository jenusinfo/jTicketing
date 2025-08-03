public interface ITicketCommentService
{
    Task<IEnumerable<TicketCommentDto>> GetByTicketIdAsync(int ticketId);
    Task<TicketCommentDto> AddCommentAsync(int ticketId, TicketCommentCreateDto dto, ClaimsPrincipal user);
}