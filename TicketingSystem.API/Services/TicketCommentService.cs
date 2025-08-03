using System.Security.Claims;

public class TicketCommentService : ITicketCommentService
{
    public Task<IEnumerable<TicketCommentDto>> GetByTicketIdAsync(int ticketId) =>
        Task.FromResult<IEnumerable<TicketCommentDto>>(new List<TicketCommentDto>());

    public Task<TicketCommentDto> AddCommentAsync(int ticketId, TicketCommentCreateDto dto, ClaimsPrincipal user) =>
        Task.FromResult(new TicketCommentDto { Id = 1, Message = dto.Message });
}