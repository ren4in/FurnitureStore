using System;
using System.Collections.Generic;

namespace FurnitureStore.db;

public partial class Customer
{
    public int IdCustomer { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string? EMail { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
