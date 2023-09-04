using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

/// <summary>
/// Tablas de consecutivos
/// </summary>
public partial class Consecutive
{
    /// <summary>
    /// Identificador del consecutivo
    /// </summary>
    [Key]
    [StringLength(5)]
    [Unicode(false)]
    public string ConsecutiveId { get; set; } = null!;

    /// <summary>
    /// Máscara del consecutivo
    /// </summary>
    [StringLength(20)]
    [Unicode(false)]
    public string Mask { get; set; } = null!;

    /// <summary>
    /// Longitud del consecutivo
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// Consecutivo disponible. Actualizar una vez sea utilizado este consecutivo
    /// </summary>
    [Column("Consecutive")]
    [StringLength(20)]
    [Unicode(false)]
    public string Consecutive1 { get; set; } = null!;
}
