using LibraryManager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using LibraryManager.Core.Repositories;

namespace LibraryManager.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly LibraryDbContext _context;

        public UserRepository(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetByCpfAsync(string cpf)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.CPF == cpf);

            return user;
        }

        public async Task<User?> ValidateUserCredentialsAsync(string cpf, string passwordHash)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.CPF == cpf && u.Password == passwordHash);
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user is not null)
            {
                user.Delete();
                await _context.SaveChangesAsync();
            }
        }
    }
}

