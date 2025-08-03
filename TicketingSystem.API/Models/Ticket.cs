using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Ticket
{
    public int Id { get; set; }

    [Required]
    public string TicketNumber { get; set; } = null!;

    [Required]
    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;
    public string Category { get; set; }
    public string Subcategory { get; set; }
    public string Priority { get; set; }
    public string Impact { get; set; }
    public string Urgency { get; set; }
    public string Status { get; set; } = "Open";
    public DateTime? DueDate { get; set; }
    public string SLA { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; }

    public int ProjectId { get; set; }
    public Project Project { get; set; }

    public int? AssignedToUserId { get; set; }
    public User? AssignedToUser { get; set; }

    public int CreatedById { get; set; }
    public User CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<TicketComment> Comments { get; set; } = new List<TicketComment>();
}
