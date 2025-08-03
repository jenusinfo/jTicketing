using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TicketingSystem.API.Requests;
// using TicketingSystem.API.DTOs; // Example, adjust as needed

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;
    private readonly ILogger<AuthService> _logger;

    public AuthService(AppDbContext context, IConfiguration config, ILogger<AuthService> logger)
    {
        _context = context;
        _config = config;
        _logger = logger;
    }

    public async Task<string> RegisterAsync(RegisterRequest dto)
    {
        try
        {
            var org = new Organization { Name = dto.OrgName };
            var user = new User
            {
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                FullName = dto.FullName,
                Role = "Admin",
                OrganizationId = org.Id
            };
            // Add organization to context first
            _context.Organizations.Add(org);
            await _context.SaveChangesAsync();
            user.OrganizationId = org.Id; // Set OrganizationId after org is saved
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return GenerateJwt(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in service.");
            throw;
        }
    }

    public async Task<string> LoginAsync(LoginRequest dto)
    {
        try
        {
            var user = await _context.Users.Include(u => u.Organization)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");
            return GenerateJwt(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in service.");
            throw;
        }
    }

    private string GenerateJwt(User user)
    {
        var jwtKey = _config["Jwt:Key"];
        if (string.IsNullOrEmpty(jwtKey))
            throw new InvalidOperationException("JWT key configuration is missing.");
        var key = Encoding.ASCII.GetBytes(jwtKey);
        var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("OrgId", user.OrganizationId.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }
}