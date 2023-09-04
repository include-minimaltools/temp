using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

/// <summary>
/// Tabla de detalle de una factura
/// </summary>
[PrimaryKey("InvoiceId", "ProductId")]
public partial class InvoiceDetail
{
    /// <summary>
    /// Identificador único de factura
    /// </summary>
    [Key]
    [StringLength(5)]
    [Unicode(false)]
    public string InvoiceId { get; set; } = null!;

    /// <summary>
    /// Identificador del producto
    /// </summary>
    [Key]
    [StringLength(5)]
    [Unicode(false)]
    public string ProductId { get; set; } = null!;

    /// <summary>
    /// Cantidad de producto
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Precio unitario del producto
    /// </summary>
    [Column(TypeName = "decimal(18, 4)")]
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Cantidad a pagar por la cantidad de este producto en esta compra
    /// </summary>
    [Column(TypeName = "decimal(29, 4)")]
    public decimal? Subtotal { get; set; }

    [ForeignKey("InvoiceId")]
    [InverseProperty("InvoiceDetail")]
    public virtual Invoice Invoice { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("InvoiceDetail")]
    public virtual Product Product { get; set; } = null!;
}
