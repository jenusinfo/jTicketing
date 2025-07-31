public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public string Role { get; set; } = "User";

    public int OrganizationId { get; set; }
    public Organization Organization { get; set; } = null!;

    public List<Comment> Comments { get; set; } = new();
    public List<Ticket> Tickets { get; set; } = new();
}
