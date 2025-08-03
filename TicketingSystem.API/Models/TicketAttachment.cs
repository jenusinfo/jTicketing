using System;
using System.ComponentModel.DataAnnotations;


public class TicketAttachment
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public DateTime UploadedAt { get; set; }
}