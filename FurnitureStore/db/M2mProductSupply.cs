using System;
using System.Collections.Generic;

namespace FurnitureStore.db;

public partial class M2mProductSupply
{
    public int IdProduct { get; set; }

    public int IdSupply { get; set; }

    public int? Amount { get; set; }

    public virtual Product? IdProductNavigation { get; set; } = null!;

    public virtual Supply? IdSupplyNavigation { get; set; } = null!;
}
