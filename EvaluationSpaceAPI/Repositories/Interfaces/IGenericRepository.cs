namespace EvaluationSpaceAPI.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task Create(T entity);
        Task Delete(T entity);
        Task DeleteRange(ICollection<T> entity);
        Task<List<T>> GetAll();
        Task<T?> GetById(Guid id);
        Task Update(T entity);
        Task<bool> SaveAsync();
    }
}