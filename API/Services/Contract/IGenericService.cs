namespace API.Services.Contract
{
    public interface IGenericService <T> where T : class
    {
        Task<List<T>> List();
        Task<T> Get(int id);
        Task<T> Save(T model);
        Task<bool> Update(T model);
        Task<bool> Delete(T model);

    }
}
