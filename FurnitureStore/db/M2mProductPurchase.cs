using System;
using System.Collections.Generic;

namespace FurnitureStore.db;

public partial class M2mProductPurchase
{
    public int IdProduct { get; set; }

    public int IdPurchase { get; set; }

    public int? Amount { get; set; }

    public virtual Product? IdProductNavigation { get; set; } = null!;

    public virtual Purchase? IdPurchaseNavigation { get; set; } = null!;
}
