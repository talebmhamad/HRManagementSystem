using HRManagementSystem.Data.DTOs;

namespace HRManagementSystem.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto?> AuthenticateAsync(string username, string password);
    }
}