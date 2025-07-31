using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/tickets")]
[Authorize]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _ticketService.GetTicketsAsync(User));

    [HttpPost]
    public async Task<IActionResult> Create(TicketCreateDto dto) => Ok(await _ticketService.CreateTicketAsync(dto, User));

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) => Ok(await _ticketService.GetTicketByIdAsync(id, User));

    [HttpPost("{id}/assign")]
    [Authorize(Roles = "Agent")]
    public async Task<IActionResult> Assign(int id) => Ok(await _ticketService.AssignTicketAsync(id, User));

    [HttpPost("{id}/status")]
    [Authorize(Roles = "Agent")]
    public async Task<IActionResult> ChangeStatus(int id, [FromQuery] string status) =>
        Ok(await _ticketService.ChangeTicketStatusAsync(id, status, User));
}
