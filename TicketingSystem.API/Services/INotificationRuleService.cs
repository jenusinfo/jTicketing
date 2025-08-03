public interface INotificationRuleService
{
    Task<IEnumerable<NotificationRuleDto>> GetAllAsync();
}