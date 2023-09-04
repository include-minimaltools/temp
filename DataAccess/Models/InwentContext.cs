using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

public partial class InwentContext : DbContext
{
    public InwentContext()
    {
    }

    public InwentContext(DbContextOptions<InwentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branch { get; set; }

    public virtual DbSet<Category> Category { get; set; }

    public virtual DbSet<Consecutive> Consecutive { get; set; }

    public virtual DbSet<Inventory> Inventory { get; set; }

    public virtual DbSet<Invoice> Invoice { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetail { get; set; }

    public virtual DbSet<Logs> Logs { get; set; }

    public virtual DbSet<Product> Product { get; set; }

    public virtual DbSet<Purchase> Purchase { get; set; }

    public virtual DbSet<PurchaseDetail> PurchaseDetail { get; set; }

    public virtual DbSet<Role> Role { get; set; }

    public virtual DbSet<Supplier> Supplier { get; set; }

    public virtual DbSet<User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("pk_Branch");

            entity.ToTable(tb => tb.HasComment("Tabla de sucursales del sistema"));

            entity.Property(e => e.BranchId).HasComment("Identificador único de la sucursal");
            entity.Property(e => e.Active).HasComment("Está la sucursal activa");
            entity.Property(e => e.Address).HasComment("Dirección de la sucursal");
            entity.Property(e => e.Name).HasComment("Nombre de la sucursal");
            entity.Property(e => e.Phone).HasComment("Número de teléfono de la sucursal");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("pk_Category");

            entity.ToTable(tb => tb.HasComment("Tabla de categoría de producto"));

            entity.Property(e => e.CategoryId).HasComment("Identificador de categoría de producto");
            entity.Property(e => e.Description).HasComment("Breve descripción de la categoría de producto");
            entity.Property(e => e.Name).HasComment("Nombre de la categoría");
        });

        modelBuilder.Entity<Consecutive>(entity =>
        {
            entity.HasKey(e => e.ConsecutiveId).HasName("Pk_Consecutive_ConsecutiveId");

            entity.ToTable(tb => tb.HasComment("Tablas de consecutivos"));

            entity.Property(e => e.ConsecutiveId).HasComment("Identificador del consecutivo");
            entity.Property(e => e.Consecutive1).HasComment("Consecutivo disponible. Actualizar una vez sea utilizado este consecutivo");
            entity.Property(e => e.Length).HasComment("Longitud del consecutivo");
            entity.Property(e => e.Mask).HasComment("Máscara del consecutivo");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("pk_Inventory");

            entity.ToTable(tb => tb.HasComment("Tabla de inventario de productos de cada una de las sucursales"));

            entity.Property(e => e.InventoryId).HasComment("Identificador de la relación producto - sucursal");
            entity.Property(e => e.BranchId).HasComment("Identificador único de la sucursal");
            entity.Property(e => e.ProductId).HasComment("Identificador del producto");
            entity.Property(e => e.Stock).HasComment("Existencias del producto en la sucursal establecida");

            entity.HasOne(d => d.Branch).WithMany(p => p.Inventory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Inventory_Branch");

            entity.HasOne(d => d.Product).WithMany(p => p.Inventory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Inventory_Product");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("pk_Invoice");

            entity.ToTable(tb => tb.HasComment("Tabla de encabezado de facturas"));

            entity.Property(e => e.InvoiceId).HasComment("Identificador único de factura");
            entity.Property(e => e.Date).HasComment("Fecha en la que se generó la factura");
            entity.Property(e => e.Discount).HasComment("Monto de descuento ofrecido al subtotal de esta factura");
            entity.Property(e => e.Subtotal).HasComment("Suma del valor bruto de los productos");
            entity.Property(e => e.Tax).HasComment("Monto de impuesto aplicado al Subtotal - Discount");
            entity.Property(e => e.Total)
                .HasComputedColumnSql("(([Subtotal]-[Discount])+[Tax])", false)
                .HasComment("Monto total a pagar por el cliente");

            entity.HasOne(d => d.Branch).WithMany(p => p.Invoice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice__BranchI__2A4B4B5E");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => new { e.InvoiceId, e.ProductId }).HasName("pk_InvoiceDetail");

            entity.ToTable(tb => tb.HasComment("Tabla de detalle de una factura"));

            entity.Property(e => e.InvoiceId).HasComment("Identificador único de factura");
            entity.Property(e => e.ProductId).HasComment("Identificador del producto");
            entity.Property(e => e.Quantity).HasComment("Cantidad de producto");
            entity.Property(e => e.Subtotal)
                .HasComputedColumnSql("([UnitPrice]*[Quantity])", false)
                .HasComment("Cantidad a pagar por la cantidad de este producto en esta compra");
            entity.Property(e => e.UnitPrice).HasComment("Precio unitario del producto");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InvoiceDetail_Invoice");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceDetail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InvoiceDetail_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("pk_Product");

            entity.ToTable(tb => tb.HasComment("Tabla de productos del sistema"));

            entity.Property(e => e.ProductId).HasComment("Identificador del producto");
            entity.Property(e => e.Barcode).HasComment("Código de barra asociado a este producto");
            entity.Property(e => e.Description).HasComment("Breve descripción del producto");
            entity.Property(e => e.MinStock).HasComment("Existencias mínimas del producto");
            entity.Property(e => e.PhotoUri).HasComment("Dirección de la imagen del producto");
            entity.Property(e => e.Price).HasComment("Precio actual del producto");
            entity.Property(e => e.Type)
                .IsFixedLength()
                .HasComment("Tipo de producto. I - Artículo | S - Servicio");

            entity.HasMany(d => d.Category).WithMany(p => p.Product)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("fk_ProductCategory_Category"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .HasConstraintName("fk_ProductCategory_Product"),
                    j =>
                    {
                        j.HasKey("ProductId", "CategoryId").HasName("pk_ProductCategory");
                        j.ToTable(tb => tb.HasComment("Tabla relacional de producto con categoría"));
                        j.IndexerProperty<string>("ProductId")
                            .HasMaxLength(5)
                            .IsUnicode(false)
                            .HasComment("Identificador del producto");
                        j.IndexerProperty<int>("CategoryId").HasComment("Identificador de categoría de producto");
                    });
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("pk_Purchase");

            entity.ToTable(tb => tb.HasComment("Tabla de compras de productos"));

            entity.Property(e => e.PurchaseId).HasComment("Identificador de compra del sistema");
            entity.Property(e => e.Date).HasComment("Fecha de la compra");
            entity.Property(e => e.Discount).HasComment("Descuento ofrecido por el proveedor");
            entity.Property(e => e.Subtotal).HasComment("Monto a pagar sin impuestos ni descuento");
            entity.Property(e => e.SupplierId).HasComment("Identificador del proveedor");
            entity.Property(e => e.SupplierInvoiceId).HasComment("Identificador de la factura del proveedor");
            entity.Property(e => e.Tax).HasComment("Impuesto total a pagar por la compra");

            entity.HasOne(d => d.Branch).WithMany(p => p.Purchase)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Purchase__Branch__3C69FB99");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Purchase)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Purchase_Supplier");
        });

        modelBuilder.Entity<PurchaseDetail>(entity =>
        {
            entity.HasKey(e => new { e.PurchaseId, e.ProductId }).HasName("pk_PurchaseDetail");

            entity.ToTable(tb => tb.HasComment("Tabla de detalles de la compra"));

            entity.Property(e => e.PurchaseId).HasComment("Identificador de compra del sistema");
            entity.Property(e => e.ProductId).HasComment("Identificador del producto");
            entity.Property(e => e.Quantity).HasComment("Cantidad comprada del producto");
            entity.Property(e => e.SuggestedRetailPrice).HasComment("Precio sugerido de venta");
            entity.Property(e => e.UnitCost).HasComment("Costo unitario del producto");

            entity.HasOne(d => d.Product).WithMany(p => p.PurchaseDetail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_PurchaseDetail_Product");

            entity.HasOne(d => d.Purchase).WithMany(p => p.PurchaseDetail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_PurchaseDetail_Purchase");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("Pk_Role_IdRole");

            entity.ToTable(tb => tb.HasComment("Tabla de roles del sistema"));

            entity.Property(e => e.IdRole).HasComment("Identificador único de rol");
            entity.Property(e => e.Active).HasComment("Determina si el rol está activo o inactivo");
            entity.Property(e => e.Description).HasComment("Descripción del rol");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("pk_Supplier");

            entity.ToTable(tb => tb.HasComment("Tabla de proveedores del sistema"));

            entity.Property(e => e.SupplierId).HasComment("Identificador del proveedor");
            entity.Property(e => e.Address).HasComment("Dirección del proveedor");
            entity.Property(e => e.Email).HasComment("Correo electrónico del proveedor");
            entity.Property(e => e.Name).HasComment("Nombre del proveedor");
            entity.Property(e => e.Phone).HasComment("Número telefónico del proveedor");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("pk_Tbl");

            entity.ToTable(tb => tb.HasComment("Tabla de usuarios del sistema"));

            entity.Property(e => e.Username).HasComment("Nombre identificador del usuario del sistema");
            entity.Property(e => e.Active).HasComment("Estado activo del usuario");
            entity.Property(e => e.DefaultBranch).HasComment("Sucursal que tomará el sistema por defecto al realizar las facturas o compras");
            entity.Property(e => e.Firstname).HasComment("Nombres de pila del usuario");
            entity.Property(e => e.LastLogin).HasComment("Último inicio de sesión del usuario");
            entity.Property(e => e.Lastname).HasComment("Apellidos del usuario");
            entity.Property(e => e.Password).HasComment("Contraseña del usuario encriptada");

            entity.HasOne(d => d.DefaultBranchNavigation).WithMany(p => p.User).HasConstraintName("FK__User__DefaultBra__33D4B598");

            entity.HasMany(d => d.Role).WithMany(p => p.User)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("Role")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Fk_UserRole_Role"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("User")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Fk_UserRole_User"),
                    j =>
                    {
                        j.HasKey("User", "Role").HasName("Idx_UserRole");
                        j.ToTable(tb => tb.HasComment("Tabla relacional de roles de usuario"));
                        j.IndexerProperty<string>("User")
                            .HasMaxLength(20)
                            .IsUnicode(false)
                            .HasComment("Nombre identificador del usuario del sistema");
                        j.IndexerProperty<string>("Role")
                            .HasMaxLength(10)
                            .IsUnicode(false)
                            .HasComment("Identificador único de rol");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
