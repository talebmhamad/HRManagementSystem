using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Data.Entities;
using HRManagementSystem.Repositories.Interfaces;
using HRManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HRManagementSystem.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(IUserRepository userRepository,IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDto?> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null || !user.IsActive)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(user,user.PasswordHash,password);

            if (result != PasswordVerificationResult.Success)
            {
                return null;
            }

            return new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Role = user.Role,
                IsActive = user.IsActive,
                EmployeeId = user.EmployeeId,
            };
        }
    }
}
