public class TicketAttachmentService : ITicketAttachmentService
{
    public Task<IEnumerable<TicketAttachmentDto>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<TicketAttachmentDto>>(new List<TicketAttachmentDto>());
    }
}