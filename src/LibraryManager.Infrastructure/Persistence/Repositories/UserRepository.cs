﻿using LibraryManager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using LibraryManager.Core.Repositories;

namespace LibraryManager.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _context;

        public UserRepository(LibraryDbContext context) 
		{
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();

            return users;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<User?> GetByCpfAsync(string cpf)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.CPF == cpf);

            return user;
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);

            await _context.SaveChangesAsync();
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

