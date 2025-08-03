using System.Security.Claims;

public interface INotificationService
{
    Task<IEnumerable<NotificationDto>> GetAllAsync(ClaimsPrincipal user);
    Task GetAllAsync(ClaimsPrincipal user);
    Task<bool> MarkAsReadAsync(long id, ClaimsPrincipal user);
}