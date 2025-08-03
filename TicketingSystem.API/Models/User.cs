using System;
using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; } // <-- Added 'required' modifier
    public required string FullName { get; set; }
    public required string Role { get; set; } // <-- Added 'required' modifier
    public DateTime CreatedAt { get; set; }
    public ICollection<Ticket> CreatedTickets { get; set; } = new List<Ticket>();
    public ICollection<Ticket> AssignedTickets { get; set; } = new List<Ticket>();
    public ICollection<TicketComment> Comments { get; set; } = new List<TicketComment>();
    public int OrganizationId { get; set; }
    public Organization? Organization { get; set; }
}