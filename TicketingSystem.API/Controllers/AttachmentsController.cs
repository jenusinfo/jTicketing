using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AttachmentsController : ControllerBase
{
    private readonly IAttachmentService _attachmentService;
    private readonly ILogger<AttachmentsController> _logger;

    public AttachmentsController(IAttachmentService service, ILogger<AttachmentsController> logger)
    {
        _attachmentService = service;
        _logger = logger;
    }

    [HttpPost("{ticketId}")]
    [Authorize]
    public async Task<IActionResult> Upload(int ticketId, IFormFile file)
    {
        try
        {
            if (file == null) return BadRequest("File required");

            var result = await _attachmentService.UploadAsync(ticketId, file, User);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in AttachmentsController.Upload");
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> Download(int id)
    {
        try
        {
            var result = await _attachmentService.DownloadAsync(id);
            return result ?? NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in AttachmentsController.Download");
            return StatusCode(500, "Internal server error.");
        }
    }
}
