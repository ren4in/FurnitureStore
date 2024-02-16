using System;
using System.Collections.Generic;

namespace FurnitureStore.db;

public partial class Supply
{
    public int IdSupply { get; set; }

    public int? IdProvider { get; set; }

    public DateTime SupplyDateTime { get; set; }

    public int? IdUser { get; set; }

    public virtual Provider? IdProviderNavigation { get; set; }     = null;

    public virtual User? IdUserNavigation { get; set; } = null!;

    public virtual ICollection<M2mProductSupply> M2mProductSupplies { get; set; } = new List<M2mProductSupply>();
}
