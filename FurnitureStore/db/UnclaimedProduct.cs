using System;
using System.Collections.Generic;

namespace FurnitureStore.db;

public partial class UnclaimedProduct
{
    public string Название { get; set; } = null!;

    public decimal Цена { get; set; }
}
