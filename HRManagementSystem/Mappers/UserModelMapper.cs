using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Web.Models;
using HRManagementSystem.Web.Models.User;

namespace HRManagementSystem.Web.Mappers
{
    public class UserModelMapper
    {
        public static UserViewModel ToViewModel(UserDto dto)
        {
            if (dto == null) return null!;

            return new UserViewModel
            {
                UserId = dto.UserId,
                Username = dto.Username,
                Role = dto.Role,
                IsActive = dto.IsActive,
                EmployeeName = dto.EmployeeName
            };
        }

        public static UserDto ToDto(UserEditViewModel model)
        {
            if (model == null) return null!;

            return new UserDto
            {
                UserId = model.UserId,
                Username = model.Username,
                Role = model.Role,
                IsActive = model.IsActive,
                Password = model.Password ?? string.Empty

            };
        }

        public static UserEditViewModel ToEditViewModel(UserDto dto)
        {
            if (dto == null) return null!;
            return new UserEditViewModel
            {
                UserId = dto.UserId,
                Username = dto.Username,
                Role = dto.Role,
                IsActive = dto.IsActive
            };
        }
    }
}
