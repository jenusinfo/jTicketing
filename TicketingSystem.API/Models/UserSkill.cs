using System;
using System.ComponentModel.DataAnnotations;


public class UserSkill
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string SkillName { get; set; }
}