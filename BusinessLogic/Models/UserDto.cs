
using System.ComponentModel.DataAnnotations;

public record UserRoleDto(string roleId, string description);

public record UserDto(string username, string password, string name, string lastName, UserRoleDto role);

public class UserInputDto
{
  [Required]
  public string Username { get; set; } = string.Empty;

  [Required(ErrorMessage = "La contraseña es requerida")]
  [MinLength(20, ErrorMessage = "La contraseña debe tener un valor mínimo de 20 carácteres")]
  [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@#$%^&+=!*()_])[A-Za-z\\d@#$%^&+=!*()_]{8,}$", ErrorMessage = "Debe ser segura")]
  public string Password { get; set; } = string.Empty;
}