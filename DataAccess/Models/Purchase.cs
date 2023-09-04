using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

/// <summary>
/// Tabla de compras de productos
/// </summary>
public partial class Purchase
{
    /// <summary>
    /// Identificador de compra del sistema
    /// </summary>
    [Key]
    [StringLength(5)]
    [Unicode(false)]
    public string PurchaseId { get; set; } = null!;

    /// <summary>
    /// Identificador de la factura del proveedor
    /// </summary>
    [StringLength(10)]
    [Unicode(false)]
    public string SupplierInvoiceId { get; set; } = null!;

    /// <summary>
    /// Fecha de la compra
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    /// <summary>
    /// Monto a pagar sin impuestos ni descuento
    /// </summary>
    [Column(TypeName = "decimal(18, 4)")]
    public decimal Subtotal { get; set; }

    /// <summary>
    /// Descuento ofrecido por el proveedor
    /// </summary>
    [Column(TypeName = "decimal(18, 4)")]
    public decimal Discount { get; set; }

    /// <summary>
    /// Impuesto total a pagar por la compra
    /// </summary>
    [Column(TypeName = "decimal(18, 4)")]
    public decimal Tax { get; set; }

    /// <summary>
    /// Identificador del proveedor
    /// </summary>
    [StringLength(5)]
    [Unicode(false)]
    public string SupplierId { get; set; } = null!;

    public bool Active { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string BranchId { get; set; } = null!;

    [ForeignKey("BranchId")]
    [InverseProperty("Purchase")]
    public virtual Branch Branch { get; set; } = null!;

    [InverseProperty("Purchase")]
    public virtual ICollection<PurchaseDetail> PurchaseDetail { get; set; } = new List<PurchaseDetail>();

    [ForeignKey("SupplierId")]
    [InverseProperty("Purchase")]
    public virtual Supplier Supplier { get; set; } = null!;
}
