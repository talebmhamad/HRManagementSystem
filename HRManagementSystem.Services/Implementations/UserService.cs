using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Data.Entities;
using HRManagementSystem.Repositories.Interfaces;
using HRManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HRManagementSystem.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository,IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetUsers();

            return users.Select(u => new UserDto
            {
                UserId = u.UserId,
                Username = u.Username,
                IsActive = u.IsActive,
              
            }).ToList();
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null) return null;

            return new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                IsActive = user.IsActive,
            };
        }

        public async Task<UserDto> AddUser(UserDto userDto)
        {
            var user = new User
            {
                Username = userDto.Username,
                EmployeeId = userDto.EmployeeId,
                Role = userDto.Role,
                IsActive = userDto.IsActive
            };
            // Hash password
            user.PasswordHash = _passwordHasher.HashPassword(user, userDto.PasswordHash);
            var createdUser = await _userRepository.CreateUser(user);

            return new UserDto
            {
                UserId = createdUser.UserId,
                Username = createdUser.Username,
                EmployeeId = createdUser.EmployeeId,
                Role = createdUser.Role,
                IsActive = createdUser.IsActive
            };
        }

    }
}
