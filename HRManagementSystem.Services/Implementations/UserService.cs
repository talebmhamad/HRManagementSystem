using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Data.Entities;
using HRManagementSystem.Repositories.Interfaces;
using HRManagementSystem.Services.Interfaces;
using HRManagementSystem.Services.Mappers;
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

        public async Task<List<UserDto>> GetAll()
        {
            var users = await _userRepository.GetUsers();
            return users.Select(UserMapper.ToDto).ToList();
        }

        public async Task<UserDto?> GetById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            return user == null ? null : UserMapper.ToDto(user);
        }

        public async Task<UserDto> AddUser(UserDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }

            if (string.IsNullOrWhiteSpace(userDto.Username))
            {
                throw new ArgumentException("Username is required", nameof(userDto.Username));
            }

            if (string.IsNullOrWhiteSpace(userDto.Password))
            {
                throw new ArgumentException("Password is required", nameof(userDto.Password));
            }

            var user = UserMapper.ToEntity(userDto);

            user.PasswordHash = _passwordHasher.HashPassword(user, userDto.Password);

            var createdUser = await _userRepository.CreateUser(user);

            return UserMapper.ToDto(createdUser);
        }


        public async Task<UserDto> UpdateUser(UserDto userDto)
        {
            var existingUser = await _userRepository.GetUserById(userDto.UserId);
            if (existingUser == null)
            {
                throw new ArgumentException("User not found", nameof(userDto.UserId));
            }

            existingUser.Username = userDto.Username;
            existingUser.Role = userDto.Role;
            existingUser.IsActive = userDto.IsActive;

            if (!string.IsNullOrEmpty(userDto.Password))
            {
                existingUser.PasswordHash = _passwordHasher.HashPassword(existingUser, userDto.Password);
            }
            var updatedUser = await _userRepository.UpdateUser(existingUser);
            return UserMapper.ToDto(updatedUser);
        }

        public async Task<bool> DeactivateUser(int UserId)
        {
            var existingUser = await _userRepository.GetUserById(UserId);
            if (existingUser == null)
            {
                throw new ArgumentException("User not found", nameof(UserId));
            }

            await _userRepository.DeactivateUser(UserId);
            return true;
        }

    }
}
