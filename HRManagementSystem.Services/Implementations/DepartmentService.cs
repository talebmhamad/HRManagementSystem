using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Data.Entities;
using HRManagementSystem.Repositories.Interfaces;
using HRManagementSystem.Services.Interfaces;

namespace HRManagementSystem.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<List<DepartmentDto>> GetAllDepartment()
        {
            var departments = await _departmentRepository.GetDepartments();

            return departments.Select(d => new DepartmentDto
            {
                DepartmentId = d.DepartmentId,
                Name = d.Name,
                Description = d.Description
            }).ToList();
        }

        public async Task<DepartmentDto> AddDepartment(DepartmentDto departmentDto)
        {
            var department = new Department
            {
                Name = departmentDto.Name,
                Description = departmentDto.Description
            };

            var created = await _departmentRepository.AddDepartment(department);

            return new DepartmentDto
            {
                DepartmentId = created.DepartmentId,
                Name = created.Name,
                Description = created.Description
            };
        }

        public async Task<DepartmentDto?> UpdateDepartment(DepartmentDto departmentDto)
        {
            var department = new Department
            {
                DepartmentId = departmentDto.DepartmentId,
                Name = departmentDto.Name,
                Description = departmentDto.Description
            };

            var updated = await _departmentRepository.UpdateDepartment(department);

            if (updated == null)
                return null;

            return new DepartmentDto
            {
                DepartmentId = updated.DepartmentId,
                Name = updated.Name,
                Description = updated.Description
            };
        }

        public async Task<bool> DeleteDepartment(int departmentId)
        {
            return await _departmentRepository.DeleteDepartment(departmentId);
        }
        public async Task<DepartmentDto?> GetDepartmentById(int departmentId)
        {
            var department = await _departmentRepository.GetDepartmentById(departmentId);
            if (department == null)
                return null;
            return new DepartmentDto
            {
                DepartmentId = department.DepartmentId,
                Name = department.Name,
                Description = department.Description
            };
        }

        public async Task<int> CountDepartments()
        {
            return await _departmentRepository.CountDepartments();
        }
    }
}
