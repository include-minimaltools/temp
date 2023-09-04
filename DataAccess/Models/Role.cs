using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

/// <summary>
/// Tabla de roles del sistema
/// </summary>
public partial class Role
{
    /// <summary>
    /// Identificador único de rol
    /// </summary>
    [Key]
    [StringLength(10)]
    [Unicode(false)]
    public string IdRole { get; set; } = null!;

    /// <summary>
    /// Descripción del rol
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    /// <summary>
    /// Determina si el rol está activo o inactivo
    /// </summary>
    public bool Active { get; set; }

    [ForeignKey("Role")]
    [InverseProperty("Role")]
    public virtual ICollection<User> User { get; set; } = new List<User>();
}
