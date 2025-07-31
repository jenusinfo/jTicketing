using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public class TicketService : ITicketService
{
    private readonly AppDbContext _context;

    public TicketService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<object>> GetTicketsAsync(ClaimsPrincipal user)
    {
        var orgId = int.Parse(user.FindFirst("OrgId").Value);
        return await _context.Tickets
            .Where(t => t.OrganizationId == orgId)
            .Select(t => new { t.Id, t.Title, t.Status, t.Priority, t.AssignedToId })
            .ToListAsync();
    }

    public async Task<object> CreateTicketAsync(TicketCreateDto dto, ClaimsPrincipal user)
    {
        var userId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);
        var orgId = int.Parse(user.FindFirst("OrgId").Value);
        var ticket = new Ticket
        {
            Title = dto.Title,
            Description = dto.Description,
            CreatedById = userId,
            OrganizationId = orgId,
            Status = "Open",
            Priority = "Medium"
        };
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task<object> GetTicketByIdAsync(int id, ClaimsPrincipal user)
    {
        var orgId = int.Parse(user.FindFirst("OrgId").Value);
        return await _context.Tickets
            .Where(t => t.OrganizationId == orgId && t.Id == id)
            .Include(t => t.Comments)
            .ThenInclude(c => c.User)
            .FirstOrDefaultAsync();
    }

    public async Task<object> AssignTicketAsync(int ticketId, ClaimsPrincipal user)
    {
        var userId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);
        var ticket = await _context.Tickets.FindAsync(ticketId);
        if (ticket == null) throw new Exception("Ticket not found");
        ticket.AssignedToId = userId;
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task<object> ChangeTicketStatusAsync(int ticketId, string newStatus, ClaimsPrincipal user)
    {
        var ticket = await _context.Tickets.FindAsync(ticketId);
        if (ticket == null) throw new Exception("Ticket not found");
        ticket.Status = newStatus;
        await _context.SaveChangesAsync();
        return ticket;
    }
}
