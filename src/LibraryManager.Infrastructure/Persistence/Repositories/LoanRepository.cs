using LibraryManager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using LibraryManager.Core.Repositories;

namespace LibraryManager.Infrastructure.Persistence.Repositories
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        private readonly LibraryDbContext _context;

        public LoanRepository(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Loan>> GetLoansByBookIdAsync(Guid bookId)
        {
            return await _context.Loans.Where(l => l.BookId == bookId).ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetLoansByUserIdAsync(Guid userId)
        {
            return await _context.Loans.Where(l => l.UserId == userId).ToListAsync();
        }
    }
}
