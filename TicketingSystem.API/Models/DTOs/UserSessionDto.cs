using System;
using System.ComponentModel.DataAnnotations;

public class UserSessionDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpiresAt { get; set; }
}