using LibraryManager.Core.Entities;

namespace LibraryManager.Core.Repositories
{
    public interface ILoanRepository
    {
        Task AddAsync(Loan loan);
        Task UpdateAsync(Loan loan);
        Task<Loan?> GetByIdAsync(Guid id);
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<IEnumerable<Loan>> GetLoansByUserIdAsync(Guid userId);
        Task<IEnumerable<Loan>> GetLoansByBookIdAsync(Guid bookId);
    }
}
