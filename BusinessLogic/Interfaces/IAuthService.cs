
using BusinessLogic.Models;

namespace BusinessLogic.Interfaces;

public interface IAuthService
{
  public List<SupplierDto> GetSuppliers();
  
  public string Login(string username, string password);
}