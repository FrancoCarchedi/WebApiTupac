namespace WebApiTupac.Data.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(string id);
        Task Insert(T entity);
        Task Update(string id, T entity);
        Task DeleteById(string id);

    }
}
