using LibraryManager.Core.Enums;
using LibraryManager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using LibraryManager.Core.Repositories;

namespace LibraryManager.Infrastructure.Persistence.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAvailableBooksAsync()
        {
            return await _context.Books.Where(b => b.Status == BookStatus.Available).ToListAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book is not null)
            {
                book.Delete();
                await _context.SaveChangesAsync();
            }
        }
    }
}
