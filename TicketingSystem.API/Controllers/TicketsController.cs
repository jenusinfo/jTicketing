using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TicketingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ILogger<TicketsController> _logger;

        public TicketsController(ITicketService ticketService, ILogger<TicketsController> logger)
        {
            _ticketService = ticketService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tickets = await _ticketService.GetAllAsync(User);
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in TicketsController");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var ticket = await _ticketService.GetByIdAsync(id, User);
                if (ticket == null) return NotFound();
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in TicketsController");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] TicketCreateDto dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var created = await _ticketService.CreateAsync(dto, User);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in TicketsController");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] TicketUpdateDto dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var updated = await _ticketService.UpdateAsync(id, dto, User);
                if (updated == null) return NotFound();
                return Ok(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in TicketsController");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _ticketService.DeleteAsync(id, User);
                if (!deleted) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in TicketsController");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
