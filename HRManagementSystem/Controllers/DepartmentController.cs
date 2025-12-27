using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Services.Interfaces;
using HRManagementSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await departmentService.GetAllDepartment();

            var model = departments.Select(d => new Models.DepartmentViewModel
            {
                Id = d.DepartmentId,
                Name = d.Name,
                Description = d.Description ?? ""
            }).ToList();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel department )
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(department);
                }
                var dto = new DepartmentDto
                {
                    Name = department.Name,
                    Description = department.Description
                };
                await departmentService.AddDepartment(dto);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return BadRequest();

            var department = await departmentService.GetDepartmentById(id);

            if (department == null)
                return NotFound();

            var model = new DepartmentViewModel
            {
                Id = department.DepartmentId,
                Name = department.Name,
                Description = department.Description
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dto = new DepartmentDto
            {
                DepartmentId = model.Id,
                Name = model.Name,
                Description = model.Description
            };

            var updated = await departmentService.UpdateDepartment(dto);

            if (updated == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await departmentService.DeleteDepartment(id);

            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }


    }
}
