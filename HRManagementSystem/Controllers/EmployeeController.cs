using HRManagementSystem.Services.Interfaces;
using HRManagementSystem.Web.Mappers;
using HRManagementSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRManagementSystem.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService _employeeService, IDepartmentService _departmentService)
        { 
            this._employeeService = _employeeService;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dto = EmployeeViewModelMapper.ToDto(model);
            var result = await _employeeService.AddEmployee(dto);

            if (result == null)
            {
                ModelState.AddModelError("", "Failed to create employee.");
                return View(model);
            }

            TempData["SuccessMessage"] = "Employee created successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employeeDto = await _employeeService.GetEmployeeById(id);
            if (employeeDto == null)
                return NotFound();

            var departments = await _departmentService.GetAllDepartment();

            var model = EmployeeViewModelMapper.ToViewModel(employeeDto, departments);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.GetAllDepartment();
                model.Departments = departments.Select(d => new SelectListItem
                {
                    Value = d.DepartmentId.ToString(),
                    Text = d.Name
                }).ToList();

                return View(model);
            }

            var dto = EmployeeViewModelMapper.ToDto(model);
            var updated = await _employeeService.UpdateEmployee(dto);

            if (updated == null)
            {
                ModelState.AddModelError("", "Failed to update employee.");
                return View(model);
            }

            TempData["SuccessMessage"] = "Employee updated successfully.";
            return RedirectToAction(nameof(Index));
        }

    }
}
