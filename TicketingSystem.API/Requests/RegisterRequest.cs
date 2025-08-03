using System;
using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.API.Requests; 
public class RegisterRequest
{
    public string OrgName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
