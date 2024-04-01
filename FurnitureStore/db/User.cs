using System;
using System.Collections.Generic;

namespace FurnitureStore.db;

public partial class User
{
    public int IdUser { get; set; }

    public int IdRole { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string Sex { get; set; } = null!;

    public int? IdPosition { get; set; }

    public int? IdDepartment { get; set; }

    public decimal? Salary { get; set; }

   

    public virtual Department? IdDepartmentNavigation { get; set; } = null!;

    public virtual Position? IdPositionNavigation { get; set; } = null!;

    public virtual Role? IdRoleNavigation { get; set; } = null!;

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

   public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

   public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
