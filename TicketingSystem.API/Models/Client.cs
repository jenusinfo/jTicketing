using System;
using System.ComponentModel.DataAnnotations;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ContactName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Project> Projects { get; set; }
    public ICollection<Ticket> Tickets { get; set; }
}