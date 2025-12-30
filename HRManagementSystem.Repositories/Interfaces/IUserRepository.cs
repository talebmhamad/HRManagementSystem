using HRManagementSystem.Data.Entities;


namespace HRManagementSystem.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserById(int id);
        Task<List<User>> GetUsers();
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);

    }
}
