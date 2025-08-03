using System;
using System.ComponentModel.DataAnnotations;

public class SlaPolicy
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ResponseTimeMinutes { get; set; }
    public int ResolutionTimeMinutes { get; set; }
}