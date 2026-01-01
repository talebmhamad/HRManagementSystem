using HRManagementSystem.Data.DTOs;


namespace HRManagementSystem.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllDepartment();
        Task<DepartmentDto> AddDepartment(DepartmentDto departmentDto);
        Task<DepartmentDto?> UpdateDepartment(DepartmentDto departmentDto);
        Task <bool> DeleteDepartment(int departmentId);
        Task<DepartmentDto?> GetDepartmentById(int departmentId);
         Task<int> CountDepartments();
    }
}
