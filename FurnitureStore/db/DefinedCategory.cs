using System;
using System.Collections.Generic;

namespace FurnitureStore.db;

public partial class DefinedCategory
{
    public string Название { get; set; } = null!;

    public string Описание { get; set; } = null!;

    public string Категория { get; set; } = null!;

    public decimal Цена { get; set; }
}
