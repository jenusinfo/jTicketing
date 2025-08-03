using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

public class AttachmentService : IAttachmentService
{
    public async Task<AttachmentDto> UploadAsync(int ticketId, IFormFile file, ClaimsPrincipal user)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File cannot be null or empty", nameof(file));

        var filePath = Path.Combine("uploads", file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return new AttachmentDto
        {
            TicketId = ticketId,
            FileName = file.FileName,
            ContentType = file.ContentType
        };
    }

    public async Task<FileStreamResult?> DownloadAsync(int attachmentId)

    {
        var filePath = Path.Combine("uploads", $"attachment_{attachmentId}.dat");

        if (!File.Exists(filePath))
            return null;

        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var contentType = "application/octet-stream";

        return new FileStreamResult(stream, contentType)
        {
            FileDownloadName = Path.GetFileName(filePath)
        };
    }
}


