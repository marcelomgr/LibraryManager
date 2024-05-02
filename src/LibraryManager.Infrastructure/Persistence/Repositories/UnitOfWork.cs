using LibraryManager.Core.Repositories;

namespace LibraryManager.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;

        public UnitOfWork(LibraryDbContext context, IBookRepository bookRepository, IUserRepository userRepository, ILoanRepository loanRepository)
        {
            _context = context;
            Books = bookRepository;
            Users = userRepository;
            Loans = loanRepository;
        }

        public IBookRepository Books { get; }
        public IUserRepository Users { get; }
        public ILoanRepository Loans { get; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
