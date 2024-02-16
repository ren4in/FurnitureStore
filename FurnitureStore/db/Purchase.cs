using System;
using System.Collections.Generic;

namespace FurnitureStore.db;

public partial class Purchase
{
    public int IdPurchase { get; set; }

    public int? IdCustomer { get; set; }

    public DateTime PurchaseDateTime { get; set; }

    public bool Delivery { get; set; }

    public string? Street { get; set; }

    public string? House { get; set; }

    public string? Housing { get; set; }

    public int? Apartment { get; set; }

    public string Status { get; set; } = null!;

    public string? Comment { get; set; }

    public int? IdUser { get; set; }

    public virtual Customer? IdCustomerNavigation { get; set; } = null!;

    public virtual User? IdUserNavigation { get; set; } = null!;

    public virtual ICollection<M2mProductPurchase> M2mProductPurchases { get; set; } = new List<M2mProductPurchase>();
}
