using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Data.Entities;

namespace HRManagementSystem.Services.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(User user)
        {
            if (user == null) return null!;

            return new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                IsActive = user.IsActive,
                Role = user.Role,
                EmployeeId = user.EmployeeId,
                EmployeeName = $"{user.Employee.FirstName} {user.Employee.LastName}"
            };
        }

        public static User ToEntity(UserDto dto)
        {
            if (dto == null) return null!;

            return new User
            {
                UserId = dto.UserId,
                Username = dto.Username,
                EmployeeId = dto.EmployeeId,
                Role = dto.Role,
                IsActive = dto.IsActive
            };
        }
    }
}
