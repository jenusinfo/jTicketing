public class AttachmentService : IAttachmentService
{
    public Task<AttachmentDto> UploadAsync(int ticketId, IFormFile file, ClaimsPrincipal user) =>
        Task.FromResult(new AttachmentDto { Id = 1, FileName = file.FileName });

    public Task<FileStreamResult?> DownloadAsync(int attachmentId) => Task.FromResult<FileStreamResult?>(null);
}