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
            .Select(t => new { t.Id, t.Title, t.Status, t.Priority })
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
            .FirstOrDefaultAsync();
    }
}
