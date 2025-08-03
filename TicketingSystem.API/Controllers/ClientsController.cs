public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly ILogger<ClientsController> _logger;

    public ClientsController(IClientService clientService, ILogger<ClientsController> logger)
    {
        _clientService = clientService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll()
    {
        try
        {
        var clients = await _clientService.GetAllAsync();
        return Ok(clients);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in ClientsController");
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<ClientDto>> GetById(int id)
    {
        try
        {
        var client = await _clientService.GetByIdAsync(id);
        if (client == null) return NotFound();
        return Ok(client);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in ClientsController");
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] ClientCreateDto dto)
    {
        try
        {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _clientService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        try
        {
    }

    [HttpPut("{id}")]
        try
        {
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] ClientUpdateDto dto)
    {
        try
        {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
        var updated = await _clientService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
        {
            _logger.LogError(ex, "An error occurred in controller.");
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
        try
        {
        var deleted = await _clientService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
        {
            _logger.LogError(ex, "An error occurred in controller.");
            return StatusCode(500, "Internal server error.");
        }
    }
}