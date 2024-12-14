using EvaluationSpaceAPI.EFContext;
using EvaluationSpaceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EvaluationSpaceAPI.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly EvaluationSpaceDbContext _context;

        public GenericRepository(EvaluationSpaceDbContext context)
        {
            _context = context;
        }

        public async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task DeleteRange(ICollection<T> entity)
        {
            _context.Set<T>().RemoveRange(entity);
            await Task.CompletedTask;
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await Task.CompletedTask;
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
