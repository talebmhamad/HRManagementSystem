using HRManagementSystem.Data;
using HRManagementSystem.Data.Entities;
using HRManagementSystem.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HRManagementSystem.Repositories.Implementations
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HRDbContext _context;
        private readonly string _connectionString;

        public DepartmentRepository(HRDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("DefaultConnection string is not configured.");
        }

        public async Task<List<Department>> GetDepartments()
        {
            return await _context.Departments.ToListAsync();
        }
        public async Task<Department> AddDepartment(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }
        public async Task<Department?> UpdateDepartment(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
            return department;
        }
        public async Task<bool> DeleteDepartment(int departmentId)
        {
            var department = await _context.Departments.FindAsync(departmentId);
            if (department == null)
            {
                return false;
            }
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Department?> GetDepartmentById(int departmentId)
        {
            return await _context.Departments.FindAsync(departmentId);
        }

        public async Task<int> CountDepartments()
        {
            int count = 0;
            using(var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM Departments";
                    var result = await command.ExecuteScalarAsync();
                    if (result != null) { count = Convert.ToInt16(result); };
                    return count;
                }
            }
        }
    }
}
