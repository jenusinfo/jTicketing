public interface IUserSessionService
{
    Task<IEnumerable<UserSessionDto>> GetAllAsync();
}