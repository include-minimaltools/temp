using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

/// <summary>
/// Tabla de categoría de producto
/// </summary>
public partial class Category
{
    /// <summary>
    /// Identificador de categoría de producto
    /// </summary>
    [Key]
    public int CategoryId { get; set; }

    /// <summary>
    /// Nombre de la categoría
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Breve descripción de la categoría de producto
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [ForeignKey("CategoryId")]
    [InverseProperty("Category")]
    public virtual ICollection<Product> Product { get; set; } = new List<Product>();
}
