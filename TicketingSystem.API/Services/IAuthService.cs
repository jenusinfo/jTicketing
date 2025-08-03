using TicketingSystem.API.Requests;

namespace TicketingSystem.API.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterRequest dto);
        Task<string> LoginAsync(LoginRequest dto);
    }
}