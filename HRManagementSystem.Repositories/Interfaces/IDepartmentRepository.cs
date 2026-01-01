using HRManagementSystem.Data.Entities;

namespace HRManagementSystem.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetDepartments();
        Task<Department> AddDepartment(Department department);
        Task<Department?> UpdateDepartment(Department department);

        Task<Department?> GetDepartmentById(int departmentId);  

        Task<bool> DeleteDepartment(int departmentId);
        Task<int> CountDepartments();

    }
}
