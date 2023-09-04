using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Models;

public record BranchDto(string BranchId, string Name, string? Address);

public partial class BranchInputDto
{
    [StringLength(5)]
    public string BranchId { get; set; } = null!;

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(200)]
    public string? Address { get; set; }

    public bool Active { get; set; }

    [StringLength(15)]
    public string? Phone { get; set; }
}
