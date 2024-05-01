using LibraryManager.Core.Entities;

namespace LibraryManager.Core.Repositories
{
    public interface IUserRepository
	{
		Task AddAsync(User user);
		Task UpdateAsync(User user);
		Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
		Task<User?> GetByCpfAsync(string cpf);
		Task<User?> ValidateUserCredentialsAsync(string cpf, string passwordHash);
		Task DeleteAsync(Guid id);
    }
}

