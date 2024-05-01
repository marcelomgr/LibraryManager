using LibraryManager.Core.Entities;

namespace LibraryManager.Core.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetAvailableBooksAsync();
        Task DeleteAsync(Guid id);
    }
}
