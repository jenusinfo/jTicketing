using System;
using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.API.Requests;

public class RegisterRequest
{
    [Required]
    public string OrgName { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string FullName { get; set; }
}
