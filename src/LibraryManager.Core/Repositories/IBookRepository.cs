using LibraryManager.Core.Entities;

namespace LibraryManager.Core.Repositories
{
    public interface IBookRepository
    {
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(Guid id);
        Task<IEnumerable<Book>> GetAvailableBooksAsync();
        Task DeleteAsync(Guid id);
    }
}
