public interface ITicketAttachmentService
{
    Task<IEnumerable<TicketAttachmentDto>> GetAllAsync();
}