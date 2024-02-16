using System;
using System.Collections.Generic;

namespace FurnitureStore.db;

public partial class Provider
{
    public int IdProvider { get; set; }

    public string ProviderName { get; set; } = null!;

    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
