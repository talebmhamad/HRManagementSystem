using HRManagementSystem.Data.DTOs;

namespace HRManagementSystem.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> AddEmployee(EmployeeDto employeeDto);
        Task<EmployeeDto?> UpdateEmployee(EmployeeDto employeeDto);
        Task<EmployeeDto?> GetEmployeeById(int employeeId);
        Task<List<EmployeeDto>> GetAllEmployees();

        Task<int> GetTotalEmployeeCount();  
         Task<int> GetActiveEmployeeCount();
    }
}
