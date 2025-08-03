using System;
using System.ComponentModel.DataAnnotations;


public class TimeEntryDto
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public int UserId { get; set; }
    public decimal HoursSpent { get; set; }
    public string Description { get; set; }
    public DateTime EntryDate { get; set; }
}