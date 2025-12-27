using HRManagementSystem.Data;
using HRManagementSystem.Data.Entities;
using HRManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users
                .Include(u => u.Employee)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users
                .Include(u => u.Employee)
                .ToListAsync();
        }
        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }

}
