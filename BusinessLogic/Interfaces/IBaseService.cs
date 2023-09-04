namespace BusinessLogic.Interfaces;

public interface IBaseService<T, Z>
{
    public Task<bool> Create(Z model);

    public Task<List<T>> Get();

    public Task<T> Get(string id);

    public Task<bool> Delete(string id);

    public Task<bool> Update(string id, Z model);
}