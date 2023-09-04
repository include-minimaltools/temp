
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Utils;
using DataAccess.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogic.Services;

public class AuthService : IAuthService
{
  private readonly TokenSettings _tokenSettings;
  private readonly InwentContext _dbContext;

  public AuthService(IOptions<TokenSettings> tokenSettings, InwentContext dbContext)
  {
    _tokenSettings = tokenSettings.Value;
    _dbContext = dbContext;
  }

  public List<SupplierDto> GetSuppliers()
  {
    var suppliers = _dbContext.Supplier.Select(x => new SupplierDto(x.SupplierId, x.Name)).ToList();
    return suppliers;
  }
  
  public string Login(string username, string password)
  {
    var issuer = _tokenSettings.IssuerToken;
    var audience = _tokenSettings.AudienceToken;
    var securityKey = _tokenSettings.SecurityKey;
    var expireTime = _tokenSettings.ExpireTime;

    if (username != "admin" || password != "admin")
      return string.Empty;
      
    SymmetricSecurityKey symmetricSecurityKey = new(Encoding.Unicode.GetBytes(securityKey));

    SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

    Claim nameIdentifier = new(ClaimTypes.NameIdentifier, username);
    Claim role = new(ClaimTypes.Role, "usuario");

    ClaimsIdentity claimsIdentity = new(new[] { nameIdentifier, role });

    var handler = new JwtSecurityTokenHandler();

    var jwt = handler.CreateJwtSecurityToken(
      issuer,
      audience,
      claimsIdentity,
      DateTime.UtcNow,
      DateTime.UtcNow.AddMinutes(expireTime),
      DateTime.UtcNow,
      signingCredentials
    );

    return handler.WriteToken(jwt);
  }
}