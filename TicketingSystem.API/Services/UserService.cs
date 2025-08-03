public class UserService : IUserService
{
    public Task<IEnumerable<UserDto>> GetAllAsync() => Task.FromResult<IEnumerable<UserDto>>(new List<UserDto>());
    public Task<UserDto> GetProfileAsync(ClaimsPrincipal user) =>
        Task.FromResult(new UserDto { Id = 1, Email = user.Identity?.Name ?? "unknown@example.com" });
}