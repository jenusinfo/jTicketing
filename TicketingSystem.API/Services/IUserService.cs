using System.Security.Claims;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto> GetProfileAsync(ClaimsPrincipal user);
    Task<object?> GetProfileAsync(ClaimsPrincipal user);
}