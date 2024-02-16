﻿using System;
using System.Collections.Generic;

namespace FurnitureStore.db;

public partial class Role
{
    public int IdRole { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
