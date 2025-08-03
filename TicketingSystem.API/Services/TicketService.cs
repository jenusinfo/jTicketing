using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

public class TicketService : ITicketService
{
    private readonly AppDbContext _context;
    private readonly ILogger<TicketService> _logger;

    public TicketService(AppDbContext context, ILogger<TicketService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<TicketDto>> GetAllAsync(ClaimsPrincipal user)
    {
        try
        {
            var orgId = int.Parse(user.FindFirst("OrgId")?.Value ?? "0");

            return await _context.Tickets
                .Select(t => new TicketDto
                {
                    Id = t.Id,
                    TicketNumber = t.TicketNumber,
                    Title = t.Title,
                    Priority = t.Priority,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetAllAsync");
            throw;
        }
    }

    public async Task<TicketDto> GetByIdAsync(int id, ClaimsPrincipal user)
    {
        try
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return null;

            return new TicketDto
            {
                Id = ticket.Id,
                TicketNumber = ticket.TicketNumber,
                Title = ticket.Title,
                Priority = ticket.Priority,
                Status = ticket.Status,
                CreatedAt = ticket.CreatedAt
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetByIdAsync");
            throw;
        }
    }

    public async Task<TicketDto> CreateAsync(TicketCreateDto dto, ClaimsPrincipal user)
    {
        try
        {
            var userId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var ticket = new Ticket
            {
                TicketNumber = $"TCKT-{DateTime.UtcNow.Ticks}",
                Title = dto.Title,
                Description = dto.Description,
                Category = dto.Category,
                Subcategory = dto.Subcategory,
                Priority = dto.Priority,
                Impact = dto.Impact,
                Urgency = dto.Urgency,
                DueDate = dto.DueDate,
                SLA = dto.SLA,
                ClientId = dto.ClientId,
                ProjectId = dto.ProjectId,
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return new TicketDto
            {
                Id = ticket.Id,
                TicketNumber = ticket.TicketNumber,
                Title = ticket.Title,
                Priority = ticket.Priority,
                Status = ticket.Status,
                CreatedAt = ticket.CreatedAt
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateAsync");
            throw;
        }
    }

    public async Task<bool> AssignToAgentAsync(int ticketId, int agentId)
    {
        try
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);
            if (ticket == null) return false;

            ticket.AssignedToUserId = agentId;
            ticket.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in AssignToAgentAsync");
            throw;
        }
    }
}
