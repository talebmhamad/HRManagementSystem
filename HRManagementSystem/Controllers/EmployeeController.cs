using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Services.Implementations;
using HRManagementSystem.Services.Interfaces;
using HRManagementSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRManagementSystem.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IUserService userService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService _employeeService,IUserService userService, IDepartmentService _departmentService)
        { 
            this._employeeService = _employeeService;
            this.userService = userService;
            this._departmentService = _departmentService;
        }

        public ActionResult Index()
        {
            var employees =  _employeeService.GetAllEmployees().Result;

            var model = employees.Select(u => new EmployeeViewModel
            {
                EmployeeId = u.EmployeeId,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                IsActive = u.IsActive,
                HasUserAccount = u.HasUserAccount,
                Department = u.DepartmentName ?? "—"
            }).ToList();

            return View(model);


        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.GetAllDepartment();

            var model = new EmployeeCreateViewModel
            {
                Departments = departments.Select(d => new SelectListItem
                {
                    Value = d.DepartmentId.ToString(),
                    Text = d.Name
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(EmployeeCreateViewModel employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            try
            {
                var dto = new EmployeeDto
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HireDate = employee.HireDate,
                    DepartmentId = employee.DepartmentId,
                    HasUserAccount = employee.CreateUserAccount,
                    IsActive = employee.IsActive,
                };

                if(dto.HasUserAccount)
                {
                    var userDto = new UserDto
                    {
                        Username = dto.Email,
                        EmployeeId = dto.EmployeeId,
                        IsActive = true,
                    };
                    await userService.AddUser(userDto);

                }
                
                await _employeeService.AddEmployee(dto);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(employee);
            }
        }










        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}
