using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

/// <summary>
/// Tabla de detalles de la compra
/// </summary>
[PrimaryKey("PurchaseId", "ProductId")]
public partial class PurchaseDetail
{
    /// <summary>
    /// Identificador de compra del sistema
    /// </summary>
    [Key]
    [StringLength(5)]
    [Unicode(false)]
    public string PurchaseId { get; set; } = null!;

    /// <summary>
    /// Identificador del producto
    /// </summary>
    [Key]
    [StringLength(5)]
    [Unicode(false)]
    public string ProductId { get; set; } = null!;

    /// <summary>
    /// Costo unitario del producto
    /// </summary>
    [Column(TypeName = "decimal(18, 4)")]
    public decimal UnitCost { get; set; }

    /// <summary>
    /// Cantidad comprada del producto
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Precio sugerido de venta
    /// </summary>
    [Column(TypeName = "decimal(18, 4)")]
    public decimal? SuggestedRetailPrice { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("PurchaseDetail")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("PurchaseId")]
    [InverseProperty("PurchaseDetail")]
    public virtual Purchase Purchase { get; set; } = null!;
}
