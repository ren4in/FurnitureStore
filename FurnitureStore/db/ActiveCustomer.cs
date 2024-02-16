using System;
using System.Collections.Generic;

namespace FurnitureStore.db;

public partial class ActiveCustomer
{
    public int КодКлиента { get; set; }

    public string Имя { get; set; } = null!;

    public string? Отчество { get; set; }

    public string Фамилия { get; set; } = null!;

    public string? ЭлектроннаяПочта { get; set; }

    public decimal? ПотраченнаяCумма { get; set; }
}
