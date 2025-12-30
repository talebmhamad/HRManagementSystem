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
            var employeesDto =  _employeeService.GetAllEmployees().Result;

            var employeeView= employeesDto.Select(EmployeeModelMapper.ToViewModel).ToList();

            return View(employeeView);


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

            var dto = EmployeeModelMapper.ToDto(model);
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

            var model = EmployeeModelMapper.ToCreateViewModel(employeeDto, departments);

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

            var dto = EmployeeModelMapper.ToDto(model);
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
