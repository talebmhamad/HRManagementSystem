using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Data.Entities;

namespace HRManagementSystem.Services.Mappers
{
    public static class EmployeeMapper
    {

        public static EmployeeDto ToDto(Employee entity)
        {
            return new EmployeeDto
            {
                EmployeeId = entity.EmployeeId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                HireDate = entity.HireDate,
                IsActive = entity.IsActive,
                HasUserAccount = entity.HasUserAccount,
                DepartmentId = entity.DepartmentId,
                DepartmentName = entity.Department?.Name ?? string.Empty,
                ProfileImagePath = entity.ProfileImagePath
            };
        }

        public static Employee ToEntity(EmployeeDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                HireDate = dto.HireDate,
                IsActive = dto.IsActive,
                HasUserAccount = dto.HasUserAccount,
                DepartmentId = dto.DepartmentId
            };
        }

        public static void UpdateEntity(Employee entity, EmployeeDto dto)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Email = dto.Email;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.HireDate = dto.HireDate;
            entity.IsActive = dto.IsActive;
            entity.HasUserAccount = dto.HasUserAccount;
            entity.DepartmentId = dto.DepartmentId;
        }

    }
}
