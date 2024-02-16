using System;
using System.Collections.Generic;

namespace FurnitureStore.db;

public partial class InexpensiveProduct
{
    public string Название { get; set; } = null!;

    public string Описание { get; set; } = null!;

    public decimal Цена { get; set; }
}
