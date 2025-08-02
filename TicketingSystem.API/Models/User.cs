public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public int OrganizationId { get; set; }
    public Organization Organization { get; set; } = null!;
    public string Role { get; set; } = string.Empty;
}
