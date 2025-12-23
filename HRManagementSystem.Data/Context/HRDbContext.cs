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
        DbSet<User> Users { get; set; } = null!;
        DbSet<Role> Roles { get; set; } = null!;
        DbSet<Attendance> Attendances { get; set; } = null!;
        DbSet<Contract> Contracts { get; set; } = null!;


    }
}
