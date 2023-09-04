
using BusinessLogic.Models;

namespace BusinessLogic.Interfaces;

public interface IBranchService : IBaseService<BranchDto, BranchInputDto>
{ 
  public string Login();
}