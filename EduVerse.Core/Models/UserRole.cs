﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EduVerse.Core.Models;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public int RoleId { get; set; }

    public int UserId { get; set; }

    public virtual Role Role { get; set; }

    public virtual UserProfile User { get; set; }
}