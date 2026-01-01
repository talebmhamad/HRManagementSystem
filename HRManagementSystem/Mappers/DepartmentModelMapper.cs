using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Web.Models.Department;

namespace HRManagementSystem.Web.Mappers
{
    public static class DepartmentModelMapper
    {
        public static DepartmentDto ToDto(DepartmentViewModel dto)
        {
            return new DepartmentDto
            {
                Description = dto.Description,
                Name = dto.Name,
                DepartmentId = dto.Id
            };
        }

        public static DepartmentViewModel ToViewModel(DepartmentDto dto)
        {
            return new DepartmentViewModel
            {
                Description = dto.Description ?? "",
                Name = dto.Name,
                Id = dto.DepartmentId
            };
        }

    }
}
