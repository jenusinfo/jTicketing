using System;
using System.ComponentModel.DataAnnotations;

public class AttachmentDto
{
    public int Id { get; set; }
    public required string FileName { get; set; }
    public int TicketId { get; set; }
    public string? ContentType { get; internal set; }
}