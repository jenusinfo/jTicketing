using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IO; // Ensure this is included

public class AttachmentService : IAttachmentService
{
    public async Task<AttachmentDto> UploadAsync(int ticketId, IFormFile file, ClaimsPrincipal user)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File cannot be null or empty", nameof(file));
        }

        var filePath = Path.Combine("uploads", file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var attachmentDto = new AttachmentDto
        {
            TicketId = ticketId,
            FileName = file.FileName,
            ContentType = file.ContentType,
            // Add other properties as needed
        };

        return attachmentDto;
    }

    public async Task<FileStreamResult?> DownloadAsync(int attachmentId)
    {
        // Dummy implementation for interface compliance
        // Replace with actual logic as needed
        return await Task.FromResult<FileStreamResult?>(null);
    }
}