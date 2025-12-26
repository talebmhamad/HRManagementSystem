using HRManagementSystem.Data.Entities;

namespace HRManagementSystem.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
    }
}
