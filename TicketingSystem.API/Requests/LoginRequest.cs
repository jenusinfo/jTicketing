using System;
using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.API.Requests; 

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
