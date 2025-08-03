using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly ILogger<UserService> _logger;

    public UserService(AppDbContext context, ILogger<UserService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        try
        {
            return await _context.Users
                .Select(u => new UserDto { Id = u.Id, Email = u.Email })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving users");
            throw;
        }
    }

    public async Task<UserDto> GetProfileAsync(ClaimsPrincipal user)
    {
        try
        {
            var idClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim == null) return null;
            var id = int.Parse(idClaim.Value);
            var entity = await _context.Users.FindAsync(id);
            if (entity == null) return null;
            return new UserDto { Id = entity.Id, Email = entity.Email };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user profile");
            throw;
        }
    }
}
