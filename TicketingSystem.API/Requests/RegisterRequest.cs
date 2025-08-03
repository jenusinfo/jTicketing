using System;
using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.API.Requests;

public class RegisterRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string OrgName { get; set; }
    public required string FullName { get; set; }
}
