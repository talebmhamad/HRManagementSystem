using HRManagementSystem.Data;
using HRManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using HRManagementSystem.Data.Entities;

namespace HRManagementSystem.Repositories.Implementations
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HRDbContext _context;

        public DepartmentRepository(HRDbContext context)
        {
            _context = context;
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
    }
}
