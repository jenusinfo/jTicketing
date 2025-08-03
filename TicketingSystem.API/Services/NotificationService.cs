public class NotificationService : INotificationService
{
    public Task<IEnumerable<NotificationDto>> GetAllAsync(ClaimsPrincipal user) =>
        Task.FromResult<IEnumerable<NotificationDto>>(new List<NotificationDto>());

    // Explicit implementation for the interface method with Task return type (no generic)
    Task INotificationService.GetAllAsync(ClaimsPrincipal user) =>
        Task.FromResult<IEnumerable<NotificationDto>>(new List<NotificationDto>());

    public Task<bool> MarkAsReadAsync(long id, ClaimsPrincipal user) => Task.FromResult(true);
}