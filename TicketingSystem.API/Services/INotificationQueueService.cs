public interface INotificationQueueService
{
    Task<IEnumerable<NotificationQueueDto>> GetAllAsync();
}