
using BusinessLogic.Models;
using BusinessLogic.Service;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services;

public class BranchService : BaseService<BranchDto, BranchInputDto>
{
  public BranchService(InwentContext db) : base(db)
  {
  }

  public override async Task<bool> Create(BranchInputDto model)
  {
    try
    {
      _dbContext.Branch.Add(new()
      {
        BranchId = model.BranchId,
        Address = model.Address,
        Name = model.Name,
      });

      await _dbContext.SaveChangesAsync();
      return true;
    }
    catch
    {
      return false;
    }

  }

  public override async Task<bool> Delete(string id)
  {
    try
    {
      var branch = await _dbContext.Branch.FindAsync(id);

      if (branch is null)
        return false;

      _dbContext.Entry(branch).State = EntityState.Deleted;
      await _dbContext.SaveChangesAsync();

      return true;
    }
    catch
    {
      return false;
    }
  }

  public override async Task<List<BranchDto>> Get()
  {
    return await _dbContext.Branch.Select(x => new BranchDto(x.BranchId, x.Name, x.Address)).ToListAsync();
  }

  public override async Task<BranchDto> Get(string id)
  {
    return await _dbContext.Branch.Where(x => x.BranchId.Equals(id)).Select(x => new BranchDto(x.BranchId, x.Name, x.Address)).FirstAsync();
  }

  public override async Task<bool> Update(string id, BranchInputDto model)
  {
    try
    {
      var branch = await _dbContext.Branch.Where(x => x.BranchId.Equals(id)).FirstAsync();

      if (branch is null)
        return false;

      branch.Name = model.Name;
      branch.Address = model.Address;

      _dbContext.Update(branch);
      await _dbContext.SaveChangesAsync();

      return true;
    }
    catch
    {
      return false;
    }
  }

  public string Login()
  {
    return string.Empty;
  }
}