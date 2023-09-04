using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

/// <summary>
/// Tabla de usuarios del sistema
/// </summary>
public partial class User
{
    /// <summary>
    /// Nombre identificador del usuario del sistema
    /// </summary>
    [Key]
    [StringLength(20)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    /// <summary>
    /// Contraseña del usuario encriptada
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    /// <summary>
    /// Nombres de pila del usuario
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string Firstname { get; set; } = null!;

    /// <summary>
    /// Apellidos del usuario
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string Lastname { get; set; } = null!;

    /// <summary>
    /// Último inicio de sesión del usuario
    /// </summary>
    [Column(TypeName = "date")]
    public DateTime? LastLogin { get; set; }

    /// <summary>
    /// Estado activo del usuario
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// Sucursal que tomará el sistema por defecto al realizar las facturas o compras
    /// </summary>
    [StringLength(5)]
    [Unicode(false)]
    public string? DefaultBranch { get; set; }

    [ForeignKey("DefaultBranch")]
    [InverseProperty("User")]
    public virtual Branch? DefaultBranchNavigation { get; set; }

    [ForeignKey("User")]
    [InverseProperty("User")]
    public virtual ICollection<Role> Role { get; set; } = new List<Role>();
}
