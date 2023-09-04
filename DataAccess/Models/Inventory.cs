using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

/// <summary>
/// Tabla de inventario de productos de cada una de las sucursales
/// </summary>
[Index("BranchId", "ProductId", Name = "Idx_Inventory", IsUnique = true)]
public partial class Inventory
{
    /// <summary>
    /// Identificador de la relación producto - sucursal
    /// </summary>
    [Key]
    public int InventoryId { get; set; }

    /// <summary>
    /// Identificador único de la sucursal
    /// </summary>
    [StringLength(5)]
    [Unicode(false)]
    public string BranchId { get; set; } = null!;

    /// <summary>
    /// Identificador del producto
    /// </summary>
    [StringLength(5)]
    [Unicode(false)]
    public string ProductId { get; set; } = null!;

    /// <summary>
    /// Existencias del producto en la sucursal establecida
    /// </summary>
    public int Stock { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("Inventory")]
    public virtual Branch Branch { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("Inventory")]
    public virtual Product Product { get; set; } = null!;
}
