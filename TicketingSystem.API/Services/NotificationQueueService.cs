public class NotificationQueueService : INotificationQueueService
{
    public Task<IEnumerable<NotificationQueueDto>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<NotificationQueueDto>>(new List<NotificationQueueDto>());
    }
}