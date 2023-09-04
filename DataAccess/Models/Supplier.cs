using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

/// <summary>
/// Tabla de proveedores del sistema
/// </summary>
public partial class Supplier
{
    /// <summary>
    /// Identificador del proveedor
    /// </summary>
    [Key]
    [StringLength(5)]
    [Unicode(false)]
    public string SupplierId { get; set; } = null!;

    /// <summary>
    /// Nombre del proveedor
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Número telefónico del proveedor
    /// </summary>
    [StringLength(15)]
    [Unicode(false)]
    public string? Phone { get; set; }

    /// <summary>
    /// Dirección del proveedor
    /// </summary>
    [StringLength(200)]
    [Unicode(false)]
    public string Address { get; set; } = null!;

    /// <summary>
    /// Correo electrónico del proveedor
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    public bool Active { get; set; }

    [InverseProperty("Supplier")]
    public virtual ICollection<Purchase> Purchase { get; set; } = new List<Purchase>();
}
