using System.Linq.Expressions;
using LibraryManager.Core.Entities;

namespace LibraryManager.Core.Repositories
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        Task<IEnumerable<Loan>> GetLoansByUserIdAsync(Guid userId, params Expression<Func<Loan, object>>[] includes);
        Task<IEnumerable<Loan>> GetLoansByBookIdAsync(Guid bookId, params Expression<Func<Loan, object>>[] includes);
    }
}
