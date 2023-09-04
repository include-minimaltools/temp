using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

/// <summary>
/// Tabla de productos del sistema
/// </summary>
public partial class Product
{
    /// <summary>
    /// Identificador del producto
    /// </summary>
    [Key]
    [StringLength(5)]
    [Unicode(false)]
    public string ProductId { get; set; } = null!;

    /// <summary>
    /// Breve descripción del producto
    /// </summary>
    [StringLength(200)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    /// <summary>
    /// Tipo de producto. I - Artículo | S - Servicio
    /// </summary>
    [StringLength(1)]
    [Unicode(false)]
    public string Type { get; set; } = null!;

    /// <summary>
    /// Código de barra asociado a este producto
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? Barcode { get; set; }

    /// <summary>
    /// Precio actual del producto
    /// </summary>
    [Column(TypeName = "decimal(18, 4)")]
    public decimal Price { get; set; }

    /// <summary>
    /// Existencias mínimas del producto
    /// </summary>
    public int? MinStock { get; set; }

    /// <summary>
    /// Dirección de la imagen del producto
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? PhotoUri { get; set; }

    public bool Active { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();

    [InverseProperty("Product")]
    public virtual ICollection<InvoiceDetail> InvoiceDetail { get; set; } = new List<InvoiceDetail>();

    [InverseProperty("Product")]
    public virtual ICollection<PurchaseDetail> PurchaseDetail { get; set; } = new List<PurchaseDetail>();

    [ForeignKey("ProductId")]
    [InverseProperty("Product")]
    public virtual ICollection<Category> Category { get; set; } = new List<Category>();
}
