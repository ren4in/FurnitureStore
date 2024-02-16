using System;
using System.Collections.Generic;

namespace FurnitureStore.db;

public partial class Inventory
{
    public int IdInventory { get; set; }

    public DateTime InventoryDateTime { get; set; }

    public bool Status { get; set; }

    public string? Comment { get; set; }

    public int? IdUser { get; set; }

    public virtual User? IdUserNavigation { get; set; } = null!;

    public virtual ICollection<M2mInventoryProduct> M2mInventoryProducts { get; set; } = new List<M2mInventoryProduct>();
}
