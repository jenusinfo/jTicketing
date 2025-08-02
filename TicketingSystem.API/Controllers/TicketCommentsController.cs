using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TicketingSystem.API.Models;

[ApiController]
[Route("api/comments")]
[Authorize]
public class TicketCommentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TicketCommentsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{ticketId}")]
    public async Task<IActionResult> GetComments(int ticketId)
    {
        var comments = await _context.Comments
            .Where(c => c.TicketId == ticketId)
            .OrderBy(c => c.CreatedAt)
            .Select(c => new {
                c.Id,
                c.Message,
                c.CreatedAt,
                Author = c.User.Email
            })
            .ToListAsync();

        return Ok(comments);
    }

    [HttpPost("{ticketId}")]
    public async Task<IActionResult> AddComment(int ticketId, [FromBody] string message)
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null) return Unauthorized();
        var userId = int.Parse(claim.Value);
        var comment = new Comment
        {
            Message = message,
            TicketId = ticketId,
            UserId = userId
        };
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return Ok(comment);
    }
}
