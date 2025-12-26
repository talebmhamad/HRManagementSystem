using HRManagementSystem.Data.Entities;

namespace HRManagementSystem.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User?> AuthenticateAsync(string username, string password);
    }
}
