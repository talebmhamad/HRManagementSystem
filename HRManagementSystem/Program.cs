using HRManagementSystem.Data;
using HRManagementSystem.Data.Entities;
using HRManagementSystem.Repositories.Implementations;
using HRManagementSystem.Repositories.Interfaces;
using HRManagementSystem.Services.Implementations;
using HRManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// DbContext
builder.Services.AddDbContext<HRDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddAuthentication("Cookies").AddCookie("Cookies", options =>{options.LoginPath = "/Auth/Index";options.LogoutPath = "/Auth/Logout";});
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IClaimsService, ClaimsService>();
builder.Services.AddEndpointsApiExplorer();


// 🔑 Password hashing
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();

// Default → Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");

app.Run();
