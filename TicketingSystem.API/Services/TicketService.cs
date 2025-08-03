using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.API.Models;
using TicketingSystem.API.Models.DTOs;

public class TicketService : ITicketService
{
    private readonly AppDbContext _context;

    public TicketService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<object>> GetTicketsAsync(ClaimsPrincipal user)
    {
        var claim = user.FindFirst("OrgId");
        if (claim == null) throw new UnauthorizedAccessException("OrgId claim missing");
        var orgId = int.Parse(claim.Value);
        return await _context.Tickets
            .Where(t => t.OrganizationId == orgId)
            .Select(t => new { t.Id, t.Title, t.Status, t.Priority, t.AssignedToId })
            .ToListAsync();
    }

    public async Task<object> CreateTicketAsync(TicketCreateDto dto, ClaimsPrincipal user)
    {
        var userClaim = user.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaim == null) throw new UnauthorizedAccessException("UserId claim missing");
        var userId = int.Parse(userClaim.Value);
        var orgClaim = user.FindFirst("OrgId");
        if (orgClaim == null) throw new UnauthorizedAccessException("OrgId claim missing");
        var orgId = int.Parse(orgClaim.Value);
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
        var claim = user.FindFirst("OrgId");
        if (claim == null) throw new UnauthorizedAccessException("OrgId claim missing");
        var orgId = int.Parse(claim.Value);
        return await _context.Tickets
            .Where(t => t.OrganizationId == orgId && t.Id == id)
            .Include(t => t.Comments)
            .ThenInclude(c => c.User)
            .FirstOrDefaultAsync();
    }

    public async Task<object> AssignTicketAsync(int ticketId, ClaimsPrincipal user)
    {
        var userClaim = user.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaim == null) throw new UnauthorizedAccessException("UserId claim missing");
        var userId = int.Parse(userClaim.Value);

        var orgClaim = user.FindFirst("OrgId");
        if (orgClaim == null) throw new UnauthorizedAccessException("OrgId claim missing");
        var orgId = int.Parse(orgClaim.Value);

        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
        if (ticket == null) throw new Exception("Ticket not found");
        if (ticket.OrganizationId != orgId) throw new UnauthorizedAccessException("Unauthorized");

        ticket.AssignedToId = userId;
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task<object> ChangeTicketStatusAsync(int ticketId, string newStatus, ClaimsPrincipal user)
    {
        var orgClaim = user.FindFirst("OrgId");
        if (orgClaim == null) throw new UnauthorizedAccessException("OrgId claim missing");
        var orgId = int.Parse(orgClaim.Value);

        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
        if (ticket == null) throw new Exception("Ticket not found");
        if (ticket.OrganizationId != orgId) throw new UnauthorizedAccessException("Unauthorized");

        ticket.Status = newStatus;
        await _context.SaveChangesAsync();
        return ticket;
    }
}
