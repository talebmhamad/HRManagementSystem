using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Data.Entities;

namespace HRManagementSystem.Services.Mappers
{
    public static class ContractMapper
    {
        public static ContractDto? ToDto(Contract entity)
        {
            if (entity == null) { return null; }

            return new ContractDto
            {
                ContractId = entity.ContractId,
                EmployeeId = entity.EmployeeId,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                BaseSalary = entity.BaseSalary,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt
            };
        }

        public static Contract? ToEntity(this ContractDto? dto)
        {
            if (dto is null) return null;

            return new Contract
            {
                ContractId = dto.ContractId,
                EmployeeId = dto.EmployeeId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                BaseSalary = dto.BaseSalary,
                IsActive = dto.IsActive,
            };
        }

    }
}
