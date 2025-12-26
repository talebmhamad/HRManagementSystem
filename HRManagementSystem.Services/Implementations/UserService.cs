using HRManagementSystem.Data.Entities;
using HRManagementSystem.Repositories.Interfaces;
using HRManagementSystem.Services.Interfaces;

namespace HRManagementSystem.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetUserById(id);
        }
    }
}
