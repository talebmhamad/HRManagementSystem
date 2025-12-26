using HRManagementSystem.Data.DTOs;

namespace HRManagementSystem.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> AddEmployeeAsync(EmployeeDto employeeDto);
        Task<EmployeeDto?> UpdateEmployeeAsync(EmployeeDto employeeDto);
        Task<EmployeeDto?> GetEmployeeByIdAsync(int employeeId);
        Task<List<EmployeeDto>> GetAllEmployeesAsync();
    }
}
