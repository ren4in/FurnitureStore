using System;
using System.Collections.Generic;

namespace FurnitureStore.db;

public partial class M2mInventoryProduct
{
    public int IdInventory { get; set; }

    public int IdProduct { get; set; }

    public bool Status { get; set; }

    public int Amount { get; set; }

    public string? Comment { get; set; }

    public virtual Inventory? IdInventoryNavigation { get; set; } = null!;

    public virtual Product? IdProductNavigation { get; set; } = null!;
}
