using System;
using System.ComponentModel.DataAnnotations;

public class TicketDto
{
    public int Id { get; set; }
    public string TicketNumber { get; set; }
    public string Title { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
    public DateTime CreatedAt { get; set; }
}