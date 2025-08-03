public class NotificationRuleService : INotificationRuleService
{
    public Task<IEnumerable<NotificationRuleDto>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<NotificationRuleDto>>(new List<NotificationRuleDto>());
    }
}