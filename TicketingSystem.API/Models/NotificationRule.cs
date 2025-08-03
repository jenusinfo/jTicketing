using System;
using System.ComponentModel.DataAnnotations;


public class NotificationRule
{
    public int Id { get; set; }
    public string Trigger { get; set; }
    public string Condition { get; set; }
    public string Action { get; set; }
}