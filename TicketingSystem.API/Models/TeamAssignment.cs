using System;
using System.ComponentModel.DataAnnotations;

public class TeamAssignment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int DepartmentId { get; set; }
}