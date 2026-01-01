using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Web.Models;
using HRManagementSystem.Web.Models.Contract;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRManagementSystem.Web.Mappers
{
    public static class ContractModelMapper
    {
        public static ContractDto ToDto(ContractViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return new ContractDto
            {
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                BaseSalary = model.BaseSalary,
                IsActive = model.IsActive,
                CreatedAt = model.CreatedAt
            };
        }

        public static ContractViewModel ToViewModel(ContractDto dto)
        {
            if (dto == null) return null!;

            return new ContractViewModel
            {
                ContractId = dto.ContractId,
                StartDate = dto.StartDate,
                EmployeeName = dto.EmployeeName,
                EndDate = dto.EndDate,
                BaseSalary = dto.BaseSalary,
                IsActive = dto.IsActive,
                CreatedAt = dto.CreatedAt
            };
        }

        public static ContractDto ToDto(ContractCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return new ContractDto
            {
                EmployeeId = model.EmployeeId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                BaseSalary = model.BaseSalary,
                IsActive = model.IsActive
            };
        }

        public static ContractDto ToDtoForUpdate(ContractCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return new ContractDto
            {
                ContractId = model.ContractId,
                EmployeeId = model.EmployeeId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                BaseSalary = model.BaseSalary,
                IsActive = model.IsActive
            };
        }

        public static ContractCreateModel ToEditModel(ContractDto dto, IEnumerable<EmployeeDto> employees)
        {
            if (dto == null) 
            { 
            throw new ArgumentNullException(nameof(dto));
            }

            return new ContractCreateModel
            {
                ContractId = dto.ContractId,
                EmployeeId = dto.EmployeeId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                BaseSalary = dto.BaseSalary,
                IsActive = dto.IsActive,
                Employees = employees.Select(e => new SelectListItem{Value = e.EmployeeId.ToString(),Text = $"{e.FirstName} {e.LastName}"}).ToList()
            };
        }
    }
}
