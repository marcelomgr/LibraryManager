
namespace LibraryManager.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }
        IUserRepository Users { get; }
        ILoanRepository Loans { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
