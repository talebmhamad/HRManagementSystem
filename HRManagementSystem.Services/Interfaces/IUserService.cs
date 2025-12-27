using HRManagementSystem.Data.DTOs;

namespace HRManagementSystem.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(int id);
        Task<UserDto> AddUser(UserDto userDto);
    }
}
