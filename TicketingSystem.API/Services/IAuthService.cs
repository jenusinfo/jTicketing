public interface IAuthService
{
    Task<string> RegisterAsync(RegisterRequest dto);
    Task<string> LoginAsync(LoginRequest dto);
}