using System;
using System.ComponentModel.DataAnnotations;


public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Ticket> Tickets { get; set; }
}