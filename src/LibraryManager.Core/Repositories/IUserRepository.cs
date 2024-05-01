using LibraryManager.Core.Entities;

namespace LibraryManager.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
		Task<User?> GetByCpfAsync(string cpf);
		Task<User?> ValidateUserCredentialsAsync(string cpf, string passwordHash);
		Task DeleteAsync(Guid id);
    }
}

