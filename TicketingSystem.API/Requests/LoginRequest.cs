using System;
using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.API.Requests;

public class LoginRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
