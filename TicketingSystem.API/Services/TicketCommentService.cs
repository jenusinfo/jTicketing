using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class TicketCommentService : ITicketCommentService
{
    private readonly AppDbContext _context;
    private readonly ILogger<TicketCommentService> _logger;

    public TicketCommentService(AppDbContext context, ILogger<TicketCommentService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<TicketCommentDto>> GetByTicketIdAsync(int ticketId)
    {
        try
        {
            return await _context.TicketComments
                .Where(c => c.TicketId == ticketId)
                .Select(c => new TicketCommentDto { Id = c.Id, Message = c.Message })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving comments");
            throw;
        }
    }

    public async Task<TicketCommentDto> AddCommentAsync(int ticketId, TicketCommentCreateDto dto, ClaimsPrincipal user)
    {
        try
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new UnauthorizedAccessException("User ID claim is missing.");

            var comment = new TicketComment
            {
                TicketId = ticketId,
                UserId = int.Parse(userIdClaim.Value),
                Message = dto.Message
            };
            _context.TicketComments.Add(comment);
            await _context.SaveChangesAsync();

            return new TicketCommentDto { Id = comment.Id, Message = comment.Message };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding comment");
            throw;
        }
    }
}
