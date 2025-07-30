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
}
