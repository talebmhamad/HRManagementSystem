using HRManagementSystem.Data;
using HRManagementSystem.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Web.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedInitialHrAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var context = scope.ServiceProvider
                .GetRequiredService<HRDbContext>();

            var passwordHasher = scope.ServiceProvider
                .GetRequiredService<IPasswordHasher<User>>();

            // Ensure database & migrations
            await context.Database.MigrateAsync();

            //  If at least one user exists → do nothing
            if (await context.Users.AnyAsync())
                return;

            //  CREATE HR DEPARTMENT
            var hrDepartment = await context.Departments
                .FirstOrDefaultAsync(d => d.Name == "HR");

            if (hrDepartment == null)
            {
                hrDepartment = new Department
                {
                    Name = "HR",
                    Description = "Human Resources Department"
                };

                context.Departments.Add(hrDepartment);
                await context.SaveChangesAsync(); 
            }

            //  CREATE HR EMPLOYEE
            var hrEmployee = new Employee
            {
                FirstName = "HR",
                LastName = "Admin",
                Email = "hr@company.com",
                PhoneNumber = "00000000",
                HireDate = DateTime.UtcNow,
                IsActive = true,
                HasUserAccount = true,
                DepartmentId = hrDepartment.DepartmentId
            };

            context.Employees.Add(hrEmployee);
            await context.SaveChangesAsync(); 

            //  CREATE HR USER
            var hrUser = new User
            {
                Username = hrEmployee.Email,
                Role = "Admin",
                IsActive = true,
                EmployeeId = hrEmployee.EmployeeId
            };

            hrUser.PasswordHash =
                passwordHasher.HashPassword(hrUser, "12345");

            context.Users.Add(hrUser);
            await context.SaveChangesAsync();
        }
    }
}
