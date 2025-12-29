using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRManagementSystem.Web.Mappers
{
    public static class EmployeeViewModelMapper
    {
        public static EmployeeDto ToDto(EmployeeCreateViewModel model)
        {
            if (model == null) { 
                throw new ArgumentNullException(nameof(model));
            }

            return new EmployeeDto
            {
                EmployeeId = model.EmployeeId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                HireDate = model.HireDate,
                DepartmentId = model.DepartmentId,
                IsActive = model.IsActive,
                HasUserAccount = model.CreateUserAccount
            };
        }

        public static EmployeeCreateViewModel ToViewModel(EmployeeDto dto,IEnumerable<DepartmentDto> departments){

            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            return new EmployeeCreateViewModel
            {
                EmployeeId = dto.EmployeeId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                HireDate = dto.HireDate,
                DepartmentId = dto.DepartmentId,
                IsActive = dto.IsActive,
                CreateUserAccount = dto.HasUserAccount,
                Departments = departments.Select(d => new SelectListItem
                {
                    Value = d.DepartmentId.ToString(),
                    Text = d.Name
                }).ToList()
            };
        }
    }
}
