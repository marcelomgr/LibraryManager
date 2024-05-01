
using System.Linq.Expressions;

namespace LibraryManager.Core.Repositories
{
    public interface IGenericRepository<T>
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T?> GetByIdAsync(Guid id);
    }
}
