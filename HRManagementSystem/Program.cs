using HRManagementSystem.Data;
using HRManagementSystem.Data.Entities;
using HRManagementSystem.Repositories.Implementations;
using HRManagementSystem.Repositories.Interfaces;
using HRManagementSystem.Services.Implementations;
using HRManagementSystem.Services.Interfaces;
using HRManagementSystem.Web.Middleware;
using HRManagementSystem.Web.Seed;
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
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();


// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddAuthentication("Cookies").AddCookie("Cookies", options =>{options.LoginPath = "/Auth/Index";options.LogoutPath = "/Auth/Logout";});
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IClaimsService, ClaimsService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//  Password hashing
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.WebHost.UseUrls(
    "http://localhost:5268",
    "http://0.0.0.0:5268"
);
var app = builder.Build();
await DatabaseSeeder.SeedInitialHrAsync(app.Services);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");

app.Run();

