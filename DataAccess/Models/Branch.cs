using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

/// <summary>
/// Tabla de sucursales del sistema
/// </summary>
public partial class Branch
{
    /// <summary>
    /// Identificador único de la sucursal
    /// </summary>
    [Key]
    [StringLength(5)]
    [Unicode(false)]
    public string BranchId { get; set; } = null!;

    /// <summary>
    /// Nombre de la sucursal
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Dirección de la sucursal
    /// </summary>
    [StringLength(200)]
    [Unicode(false)]
    public string? Address { get; set; }

    /// <summary>
    /// Está la sucursal activa
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// Número de teléfono de la sucursal
    /// </summary>
    [StringLength(15)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [InverseProperty("Branch")]
    public virtual ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();

    [InverseProperty("Branch")]
    public virtual ICollection<Invoice> Invoice { get; set; } = new List<Invoice>();

    [InverseProperty("Branch")]
    public virtual ICollection<Purchase> Purchase { get; set; } = new List<Purchase>();

    [InverseProperty("DefaultBranchNavigation")]
    public virtual ICollection<User> User { get; set; } = new List<User>();
}
