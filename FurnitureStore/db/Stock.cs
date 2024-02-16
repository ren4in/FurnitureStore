using System;
using System.Collections.Generic;

namespace FurnitureStore.db;

public partial class Stock
{
    public int IdShelf { get; set; }

    public int IdRow { get; set; }

    public int? IdProduct { get; set; }

    public virtual Product? IdProductNavigation { get; set; } = null!;
}
