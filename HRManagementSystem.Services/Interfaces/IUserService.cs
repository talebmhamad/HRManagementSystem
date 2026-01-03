using HRManagementSystem.Data.DTOs;

namespace HRManagementSystem.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAll();
        Task<UserDto?> GetById(int id);
        Task<UserDto> AddUser(UserDto userDto);
        Task<UserDto> UpdateUser(UserDto userDto);

        Task<bool> DeactivateUser(int UserId);
    }
}
