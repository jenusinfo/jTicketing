public class Ticket
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "Open"; // Open, In Progress, Resolved
    public string Priority { get; set; } = "Medium";

    public int CreatedById { get; set; }
    public User CreatedBy { get; set; }

    public int? AssignedToId { get; set; }
    public User? AssignedTo { get; set; }

    public int OrganizationId { get; set; }
    public Organization Organization { get; set; }

    public List<Comment> Comments { get; set; } = new();
}
