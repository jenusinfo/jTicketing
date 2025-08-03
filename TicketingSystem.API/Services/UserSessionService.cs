public class UserSessionService : IUserSessionService
{
    public Task<IEnumerable<UserSessionDto>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<UserSessionDto>>(new List<UserSessionDto>());
    }
}