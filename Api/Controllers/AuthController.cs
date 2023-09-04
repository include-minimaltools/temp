using System.Security.Claims;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class AuthController : ApiControllerBase
{
  private readonly IAuthService _authService;

  public AuthController(IAuthService authService)
  {
    _authService = authService;
  }

  [HttpGet("Username")]
  public ActionResult<string> GetUsername()
  {

    return Ok(HttpContext.User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty);
  }

  [HttpGet("Suppliers")]
  public ActionResult<List<SupplierDto>> GetSuppliers()
  {
    var suppliers = _authService.GetSuppliers();
    return Ok(suppliers);
  }

  [HttpPost]
  [AllowAnonymous]
  public ActionResult<string> Login(UserInputDto user, IBranchService branchService)
  {
    var token = _authService.Login(user.Username, user.Password);

    branchService.Login();

    if (string.IsNullOrEmpty(token))
      return BadRequest("No se hizo el token");
    else return Ok(token);
  }
}
