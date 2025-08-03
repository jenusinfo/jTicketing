public interface IAttachmentService
{
    Task<AttachmentDto> UploadAsync(int ticketId, IFormFile file, ClaimsPrincipal user);
    Task<FileStreamResult?> DownloadAsync(int attachmentId);
}