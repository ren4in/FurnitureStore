using System;
using System.Collections.Generic;

namespace FurnitureStore.db;

public partial class Product
{
    public int IdProduct { get; set; }

    public int? IdProductCategory { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public byte[]? Photo { get; set; }

    public double? Width { get; set; }

    public double? Length { get; set; }

    public double? Height { get; set; }

    public int? Amount { get; set; }

    public virtual ProductCategory? IdProductCategoryNavigation { get; set; } = null!;

    public virtual ICollection<M2mInventoryProduct> M2mInventoryProducts { get; set; } = new List<M2mInventoryProduct>();

    public virtual ICollection<M2mProductPurchase> M2mProductPurchases { get; set; } = new List<M2mProductPurchase>();

    public virtual ICollection<M2mProductSupply> M2mProductSupplies { get; set; } = new List<M2mProductSupply>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
