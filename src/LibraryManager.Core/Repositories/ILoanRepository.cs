using LibraryManager.Core.Entities;

namespace LibraryManager.Core.Repositories
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        Task<IEnumerable<Loan>> GetLoansByUserIdAsync(Guid userId);
        Task<IEnumerable<Loan>> GetLoansByBookIdAsync(Guid bookId);
    }
}
