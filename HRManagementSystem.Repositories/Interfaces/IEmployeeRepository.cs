using HRManagementSystem.Data.Entities;

namespace HRManagementSystem.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(int id);
        Task<List<Employee>> GetAllAsync();
        Task<Employee> AddAsync(Employee employee);
        Task<Employee?> UpdateAsync(Employee employee);

        Task<int> CountActiveEmployeesAsync();

        Task<int> CountEmployeesAsync();
    }
}
