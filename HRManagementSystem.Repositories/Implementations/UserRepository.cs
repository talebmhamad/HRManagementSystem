using HRManagementSystem.Data;
using HRManagementSystem.Data.Entities;
using HRManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace HRManagementSystem.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly HRDbContext _context;

        public UserRepository(HRDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }
    }

}
