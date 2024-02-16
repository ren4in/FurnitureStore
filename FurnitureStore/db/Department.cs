using System;
using System.Collections.Generic;

namespace FurnitureStore.db;

public partial class Department
{
    public int IdDepartment { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
