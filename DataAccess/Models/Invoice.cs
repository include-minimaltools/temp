using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

/// <summary>
/// Tabla de encabezado de facturas
/// </summary>
public partial class Invoice
{
    /// <summary>
    /// Identificador único de factura
    /// </summary>
    [Key]
    [StringLength(5)]
    [Unicode(false)]
    public string InvoiceId { get; set; } = null!;

    /// <summary>
    /// Fecha en la que se generó la factura
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    /// <summary>
    /// Suma del valor bruto de los productos
    /// </summary>
    [Column(TypeName = "decimal(18, 4)")]
    public decimal Subtotal { get; set; }

    /// <summary>
    /// Monto de descuento ofrecido al subtotal de esta factura
    /// </summary>
    [Column(TypeName = "decimal(18, 4)")]
    public decimal Discount { get; set; }

    /// <summary>
    /// Monto de impuesto aplicado al Subtotal - Discount
    /// </summary>
    [Column(TypeName = "decimal(18, 4)")]
    public decimal Tax { get; set; }

    /// <summary>
    /// Monto total a pagar por el cliente
    /// </summary>
    [Column(TypeName = "decimal(20, 4)")]
    public decimal? Total { get; set; }

    public bool Active { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string BranchId { get; set; } = null!;

    [ForeignKey("BranchId")]
    [InverseProperty("Invoice")]
    public virtual Branch Branch { get; set; } = null!;

    [InverseProperty("Invoice")]
    public virtual ICollection<InvoiceDetail> InvoiceDetail { get; set; } = new List<InvoiceDetail>();
}
