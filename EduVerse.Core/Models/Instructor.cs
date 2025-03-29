﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EduVerse.Core.Models;

public partial class Instructor
{
    public int InstructorId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Bio { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual UserProfile User { get; set; }
}