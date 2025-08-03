using System.Security.Claims;

public class NotificationService : INotificationService
{
    public Task<IEnumerable<NotificationDto>> GetAllAsync(ClaimsPrincipal user) =>
        Task.FromResult<IEnumerable<NotificationDto>>(new List<NotificationDto>());

    public Task<bool> MarkAsReadAsync(long id, ClaimsPrincipal user) =>
        Task.FromResult(true);

    Task INotificationService.GetAllAsync(ClaimsPrincipal user)
    {
        return GetAllAsync(user);
    }
}
