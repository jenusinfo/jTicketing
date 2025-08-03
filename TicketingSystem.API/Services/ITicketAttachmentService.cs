using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITicketAttachmentService
{
    Task<IEnumerable<TicketAttachmentDto>> GetAllAsync();
    Task<FileStreamResult?> DownloadAsync(int attachmentId);
}

public interface IAttachmentService
{
    Task<AttachmentDto> UploadAsync(int ticketId, IFormFile file, ClaimsPrincipal user);
    Task<FileStreamResult?> DownloadAsync(int attachmentId);
}
