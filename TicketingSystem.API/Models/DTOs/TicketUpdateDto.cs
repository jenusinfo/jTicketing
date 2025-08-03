using System;

public class TicketUpdateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Subcategory { get; set; }
    public string Priority { get; set; }
    public string Impact { get; set; }
    public string Urgency { get; set; }
    public DateTime? DueDate { get; set; }
    public string SLA { get; set; }
    public string Status { get; set; }
}
