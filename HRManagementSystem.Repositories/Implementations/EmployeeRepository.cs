using HRManagementSystem.Data;
using HRManagementSystem.Data.Entities;
using HRManagementSystem.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HRManagementSystem.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HRDbContext _context;
        private readonly string _connectionString;
        public EmployeeRepository(HRDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("DefaultConnection string is not configured.");
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            try
            {
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
                return employee;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees.AsNoTracking().Include(e => e.Department).ToListAsync();
        }

        public async Task<Employee?> UpdateAsync(Employee employee)
        {
            try
            {
                var existing = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

                if (existing == null)
                {
                    return null;
                }

                _context.Entry(existing).CurrentValues.SetValues(employee);
                await _context.SaveChangesAsync();

                return existing;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public async Task<int> CountActiveEmployeesAsync()
        {
            try
            {
                int count = 0;

                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM Employees WHERE IsActive = 1", con))
                    {
                        await con.OpenAsync();
                        count = Convert.ToInt16(await cmd.ExecuteScalarAsync());
                    }
                }

                return count;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task<int> CountEmployeesAsync()
        {
            try
            {
                int count = 0;
                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM Employees", con))
                    {
                        await con.OpenAsync();
                        count = Convert.ToInt16(await cmd.ExecuteScalarAsync());
                    }
                }
                return count;
            }
            catch (SqlException)
            {
                throw;
            }
        }


    }
}
