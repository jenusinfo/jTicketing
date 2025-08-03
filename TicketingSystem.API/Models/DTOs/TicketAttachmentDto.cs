using System;
using System.ComponentModel.DataAnnotations;

public class TicketAttachmentDto
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public DateTime UploadedAt { get; set; }
}