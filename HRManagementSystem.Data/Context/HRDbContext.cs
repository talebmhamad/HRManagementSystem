using HRManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Data
{
    public class HRDbContext : DbContext
    {
        public HRDbContext(DbContextOptions<HRDbContext> options)
            : base(options)
        {
           
        }

        DbSet<Employee> Employees { get; set; } = null!;
        DbSet<Department> Departments { get; set; } = null!;


    }
}
