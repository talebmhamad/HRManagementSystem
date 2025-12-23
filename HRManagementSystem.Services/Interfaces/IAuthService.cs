using HRManagementSystem.Data.Entities;

namespace HRManagementSystem.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string username, string password);
    }

}