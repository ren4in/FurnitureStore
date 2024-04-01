using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.db;

public partial class FurnitureStoreContext : DbContext
{
    public FurnitureStoreContext()
    {
    }

    public FurnitureStoreContext(DbContextOptions<FurnitureStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActiveCustomer> ActiveCustomers { get; set; }

    public virtual DbSet<ActiveSeller> ActiveSellers { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DefinedCategory> DefinedCategories { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<InexpensiveProduct> InexpensiveProducts { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<M2mInventoryProduct> M2mInventoryProducts { get; set; }

    public virtual DbSet<M2mProductPurchase> M2mProductPurchases { get; set; }

    public virtual DbSet<M2mProductSupply> M2mProductSupplies { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Supply> Supplies { get; set; }

    public virtual DbSet<UnclaimedProduct> UnclaimedProducts { get; set; }

    public virtual DbSet<User> Users { get; set; }

  //  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //    => optionsBuilder.UseSqlServer("Data Source=DESKTOP-34GKRI7;Initial Catalog=Furniture_Store;User ID=sa;Password=12345;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActiveCustomer>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Active_Customers");

            entity.Property(e => e.Имя).HasMaxLength(50);
            entity.Property(e => e.КодКлиента).HasColumnName("Код клиента");
            entity.Property(e => e.Отчество).HasMaxLength(50);
            entity.Property(e => e.ПотраченнаяCумма)
                .HasColumnType("money")
                .HasColumnName("Потраченная_cумма");
            entity.Property(e => e.Фамилия).HasMaxLength(50);
            entity.Property(e => e.ЭлектроннаяПочта)
                .HasMaxLength(50)
                .HasColumnName("Электронная_почта");
        });

        modelBuilder.Entity<ActiveSeller>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Active_Sellers");

            entity.Property(e => e.Имя).HasMaxLength(50);
            entity.Property(e => e.КодСотрудника).HasColumnName("Код_сотрудника");
            entity.Property(e => e.Отчество).HasMaxLength(50);
            entity.Property(e => e.Сумма).HasColumnType("money");
            entity.Property(e => e.Фамилия).HasMaxLength(50);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.IdCustomer)
                .HasName("Unique_Identifier3")
                .IsClustered(false);

            entity.ToTable("Customer");

            entity.Property(e => e.IdCustomer).HasColumnName("id_Customer");
            entity.Property(e => e.EMail)
                .HasMaxLength(50)
                .HasColumnName("E_mail");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("First_Name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("Last_Name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .HasColumnName("Middle_Name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("Phone_Number");
        });

        modelBuilder.Entity<DefinedCategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Defined_Categories");

            entity.Property(e => e.Категория).HasMaxLength(50);
            entity.Property(e => e.Название).HasMaxLength(50);
            entity.Property(e => e.Описание).HasColumnType("text");
            entity.Property(e => e.Цена).HasColumnType("money");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.IdDepartment).IsClustered(false);

            entity.ToTable("Department");

            entity.Property(e => e.IdDepartment).HasColumnName("id_Department");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<InexpensiveProduct>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Inexpensive_Products");

            entity.Property(e => e.Название).HasMaxLength(50);
            entity.Property(e => e.Описание).HasColumnType("text");
            entity.Property(e => e.Цена).HasColumnType("money");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.IdInventory);

            entity.ToTable("Inventory", tb => tb.HasTrigger("Filling_Inventory"));

            entity.Property(e => e.IdInventory).HasColumnName("id_Inventory");
            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.IdUser).HasColumnName("id_User");
            entity.Property(e => e.InventoryDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Inventory_DateTime");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Inventory_Users");
        });

        modelBuilder.Entity<M2mInventoryProduct>(entity =>
        {
            entity.HasKey(e => new { e.IdInventory, e.IdProduct }).HasName("PK_m2m_Inventory_Stock_1");

            entity.ToTable("m2m_Inventory_Product", tb => tb.HasTrigger("Update_Inventory_Status"));

            entity.Property(e => e.IdInventory).HasColumnName("id_Inventory");
            entity.Property(e => e.IdProduct).HasColumnName("id_Product");
            entity.Property(e => e.Comment).HasColumnType("text");

            entity.HasOne(d => d.IdInventoryNavigation).WithMany(p => p.M2mInventoryProducts)
                .HasForeignKey(d => d.IdInventory)
                .HasConstraintName("FK_m2m_Inventory_Product_Inventory");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.M2mInventoryProducts)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK_m2m_Inventory_Stock_Product");
        });

        modelBuilder.Entity<M2mProductPurchase>(entity =>
        {
            entity.HasKey(e => new { e.IdProduct, e.IdPurchase })
                .HasName("XPKm2m_Product_Purchase")
                .IsClustered(false);

            entity.ToTable("m2m_Product_Purchase", tb =>
                {
                    tb.HasTrigger("Purchase_Delete");
                    tb.HasTrigger("Purchase_Insert");
                    tb.HasTrigger("Purchase_Update");
                });

            entity.Property(e => e.IdProduct).HasColumnName("id_Product");
            entity.Property(e => e.IdPurchase).HasColumnName("id_Purchase");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.M2mProductPurchases)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("R_15");

            entity.HasOne(d => d.IdPurchaseNavigation).WithMany(p => p.M2mProductPurchases)
                .HasForeignKey(d => d.IdPurchase)
                .HasConstraintName("R_16");
        });

        modelBuilder.Entity<M2mProductSupply>(entity =>
        {
            entity.HasKey(e => new { e.IdProduct, e.IdSupply })
                .HasName("XPKm2m_Product_Supply")
                .IsClustered(false);

            entity.ToTable("m2m_Product_Supply", tb =>
                {
                    tb.HasTrigger("Supply_Delete");
                    tb.HasTrigger("Supply_Insert");
                    tb.HasTrigger("Supply_Update");
                });

            entity.Property(e => e.IdProduct).HasColumnName("id_Product");
            entity.Property(e => e.IdSupply).HasColumnName("id_Supply");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.M2mProductSupplies)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("R_17");

            entity.HasOne(d => d.IdSupplyNavigation).WithMany(p => p.M2mProductSupplies)
                .HasForeignKey(d => d.IdSupply)
                .HasConstraintName("R_18");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.IdPosition)
                .HasName("Unique_Identifier13")
                .IsClustered(false);

            entity.ToTable("Position");

            entity.Property(e => e.IdPosition).HasColumnName("id_Position");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct)
                .HasName("Unique_Identifier4")
                .IsClustered(false);

            entity.ToTable("Product");

            entity.Property(e => e.IdProduct).HasColumnName("id_Product");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IdProductCategory).HasColumnName("id_Product_Category");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .HasColumnName("Product_Name");

            entity.HasOne(d => d.IdProductCategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdProductCategory)
                .HasConstraintName("Includes_Belongs_to");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.IdProductCategory)
                .HasName("Unique_Identifier5")
                .IsClustered(false);

            entity.ToTable("Product_Category");

            entity.Property(e => e.IdProductCategory).HasColumnName("id_Product_Category");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasColumnName("Category_Name");
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasKey(e => e.IdProvider)
                .HasName("Unique_Identifier7")
                .IsClustered(false);

            entity.ToTable("Provider");

            entity.Property(e => e.IdProvider).HasColumnName("id_Provider");
            entity.Property(e => e.ProviderName)
                .HasMaxLength(50)
                .HasColumnName("Provider_Name");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.IdPurchase)
                .HasName("Unique_Identifier6")
                .IsClustered(false);

            entity.ToTable("Purchase");

            entity.Property(e => e.IdPurchase).HasColumnName("id_Purchase");
            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.House).HasMaxLength(10);
            entity.Property(e => e.Housing).HasMaxLength(5);
            entity.Property(e => e.IdCustomer).HasColumnName("id_Customer");
            entity.Property(e => e.IdUser).HasColumnName("id_User");
            entity.Property(e => e.PurchaseDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Purchase_DateTime");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Street).HasMaxLength(50);

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Makes_Made_by");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("R_26");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole)
                .HasName("Unique_Identifier2")
                .IsClustered(false);

            entity.ToTable("Role");

            entity.Property(e => e.IdRole).HasColumnName("id_Role");
            entity.Property(e => e.RoleName)
                .HasMaxLength(30)
                .HasColumnName("Role_Name");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => new { e.IdShelf, e.IdRow })
                .HasName("Unique_Identifier9")
                .IsClustered(false);

            entity.ToTable("Stock");

            entity.Property(e => e.IdShelf).HasColumnName("id_Shelf");
            entity.Property(e => e.IdRow).HasColumnName("id_Row");
            entity.Property(e => e.IdProduct).HasColumnName("id_Product");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Contains_in_Contains");
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.HasKey(e => e.IdSupply)
                .HasName("Unique_Identifier8")
                .IsClustered(false);

            entity.ToTable("Supply");

            entity.Property(e => e.IdSupply).HasColumnName("id_Supply");
            entity.Property(e => e.IdProvider).HasColumnName("id_Provider");
            entity.Property(e => e.IdUser).HasColumnName("id_User");
            entity.Property(e => e.SupplyDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Supply_DateTime");

            entity.HasOne(d => d.IdProviderNavigation).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.IdProvider)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Delivers_Supplied");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("R_24");
        });

        modelBuilder.Entity<UnclaimedProduct>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Unclaimed_Products");

            entity.Property(e => e.Название).HasMaxLength(50);
            entity.Property(e => e.Цена).HasColumnType("money");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser)
                .HasName("Unique_Identifier1")
                .IsClustered(false);

            entity.Property(e => e.IdUser).HasColumnName("id_User");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("First_Name");
            entity.Property(e => e.IdDepartment).HasColumnName("id_Department");
            entity.Property(e => e.IdPosition).HasColumnName("id_Position");
            entity.Property(e => e.IdRole).HasColumnName("id_Role");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("Last_Name");
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .HasColumnName("Middle_Name");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("Phone_Number");
       //     entity.Property(e => e.Prize).HasColumnType("money");
            entity.Property(e => e.Salary).HasColumnType("money");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsFixedLength();

            entity.HasOne(d => d.IdDepartmentNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdDepartment)
                .HasConstraintName("R_25");

            entity.HasOne(d => d.IdPositionNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdPosition)
                .HasConstraintName("R_19");

             entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                   .HasForeignKey(d => d.IdRole)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("Pertain_Have"); 
          }); 
           
      
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
