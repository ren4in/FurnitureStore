using System;
using System.Collections.Generic;

namespace FurnitureStore.db;

public partial class ActiveSeller
{
    public int КодСотрудника { get; set; }

    public string Имя { get; set; } = null!;

    public string? Отчество { get; set; }

    public string Фамилия { get; set; } = null!;

    public decimal? Сумма { get; set; }
}
