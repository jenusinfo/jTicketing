using System;
using System.ComponentModel.DataAnnotations;


public class NotificationQueue
{
    public int Id { get; set; }
    public string Recipient { get; set; }
    public string Message { get; set; }
    public bool IsSent { get; set; }
    public DateTime CreatedAt { get; set; }
}