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

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                password
            );

            return result == PasswordVerificationResult.Success ? user : null;
        }
    }
}
