using BusinessLogic.Interfaces;
using DataAccess.Models;

namespace BusinessLogic.Service;

public abstract class BaseService<T, Z> : IBaseService<T, Z>
{
    protected readonly InwentContext _dbContext;

    public BaseService(InwentContext db) => _dbContext = db;

    public abstract Task<bool> Create(Z model);

    public abstract Task<List<T>> Get();

    public abstract Task<T> Get(string id);

    public abstract Task<bool> Delete(string id);

    public abstract Task<bool> Update(string id, Z model);
}