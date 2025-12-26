using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Data.Entities;
using HRManagementSystem.Repositories.Interfaces;
using HRManagementSystem.Services.Interfaces;

namespace HRManagementSystem.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDto> AddEmployeeAsync(EmployeeDto dto)
        {
            if (dto == null) { 
                throw new ArgumentNullException(nameof(dto));
            }

            if (string.IsNullOrWhiteSpace(dto.FirstName)) 
            { 
                throw new ArgumentException("First name is required");
            }
            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                throw new Exception("Email is required");
            }

            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                HireDate = dto.HireDate,
                DepartmentId = dto.DepartmentId,
                IsActive = true,
                HasUserAccount = false
            };

            var savedEmployee = await _employeeRepository.AddAsync(employee);

            return MapToDto(savedEmployee);
        }

        public async Task<EmployeeDto?> UpdateEmployeeAsync(EmployeeDto dto)
        {
            if (dto == null)
            {
                throw new Exception("DTO NOT FOUND");
            }

            var existing = await _employeeRepository.GetByIdAsync(dto.EmployeeId);
            if (existing == null)
                return null;

            existing.FirstName = dto.FirstName;
            existing.LastName = dto.LastName;
            existing.Email = dto.Email;
            existing.PhoneNumber = dto.PhoneNumber;
            existing.IsActive = dto.IsActive;
            existing.DepartmentId = dto.DepartmentId;

            var updated = await _employeeRepository.UpdateAsync(existing);
            if (updated == null)
            {
                return null;
            }
            return  MapToDto(updated);
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int employeeId)
        {
            if (employeeId <= 0)
            {
                throw new Exception("Invalid employee ID");
            }

            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            return employee == null ? null : MapToDto(employee);
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            return employees.Select(MapToDto).ToList();
        }

        private static EmployeeDto MapToDto(Employee e)
        {
            return new EmployeeDto
            {
                EmployeeId = e.EmployeeId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                HireDate = e.HireDate,
                IsActive = e.IsActive,
                HasUserAccount = e.HasUserAccount,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.Department?.Name ?? "",
                ProfileImagePath = e.ProfileImagePath
            };
        }

    }
}
