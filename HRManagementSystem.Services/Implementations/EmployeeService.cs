using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Repositories.Interfaces;
using HRManagementSystem.Services.Interfaces;
using HRManagementSystem.Services.Mappers;

namespace HRManagementSystem.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserService _userService;
        public EmployeeService(IEmployeeRepository employeeRepository, IUserService userService)
        {
            _employeeRepository = employeeRepository;
            _userService = userService;
        }

        public async Task<EmployeeDto> AddEmployee(EmployeeDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var employee = EmployeeMapper.ToEntity(dto);
            var savedEmployee = await _employeeRepository.AddAsync(employee);

            if (dto.HasUserAccount)
            {
                var userDto = new UserDto
                {
                    Username = dto.Email,
                    EmployeeId = savedEmployee.EmployeeId,
                    IsActive = true,
                    Role = "Employee",
                    Password = dto.PhoneNumber

                };

                await _userService.AddUser(userDto);
            }

            return EmployeeMapper.ToDto(savedEmployee);
        }

        public async Task<EmployeeDto?> UpdateEmployee(EmployeeDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var existing = await _employeeRepository.GetByIdAsync(dto.EmployeeId);
            if (existing == null)
            {
                return null;
            }

            EmployeeMapper.UpdateEntity(existing, dto);

            var updated = await _employeeRepository.UpdateAsync(existing);
            if (updated == null)
            {
                return null;
            }
         
            if (dto.HasUserAccount)
            {
                var userDto = new UserDto
                {
                    Username = dto.Email,
                    EmployeeId = updated.EmployeeId,
                    IsActive = true,
                    Role = "Employee",
                    Password = dto.PhoneNumber 
                };

                await _userService.AddUser(userDto);
            }

            return EmployeeMapper.ToDto(updated);
        }


        public async Task<EmployeeDto?> GetEmployeeById(int employeeId)
        {
            if (employeeId <= 0)
            {
                throw new ArgumentException("Invalid employee ID", nameof(employeeId));
            }

            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            return employee == null ? null : EmployeeMapper.ToDto(employee);
        }

        public async Task<List<EmployeeDto>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllAsync();

            return employees.Select(EmployeeMapper.ToDto).ToList();
        }

        public async Task<int> GetTotalEmployeeCount()
        {
            return await _employeeRepository.CountEmployeesAsync();
        }

        public async Task<int> GetActiveEmployeeCount()
        {
            return await _employeeRepository.CountActiveEmployeesAsync();
        }


    }
}
