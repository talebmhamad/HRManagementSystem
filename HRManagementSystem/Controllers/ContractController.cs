using HRManagementSystem.Services.Implementations;
using HRManagementSystem.Services.Interfaces;
using HRManagementSystem.Web.Mappers;
using HRManagementSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRManagementSystem.Web.Controllers
{
    public class ContractController : Controller
    {
        private readonly IContractService _contractService;
        private readonly IEmployeeService _employeeService;

        public ContractController(IContractService contractService, IEmployeeService employeeService)
        {
            _contractService = contractService;
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var contractsDto = await _contractService.GetAll();
            var contractsView = contractsDto.Select(ContractModelMapper.ToViewModel).ToList();
            return View(contractsView);
        }

        public async Task<IActionResult> Create()
        {
            var employees = await _employeeService.GetAllEmployees();

            var model = new ContractCreateModel
            {
                StartDate = DateTime.Today,
                Employees = employees.Select(e => new SelectListItem
                {
                    Value = e.EmployeeId.ToString(),
                    Text = $"{e.FirstName} {e.LastName}"
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContractCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                var employees = await _employeeService.GetAllEmployees();
                model.Employees = employees.Select(e => new SelectListItem
                {
                    Value = e.EmployeeId.ToString(),
                    Text = $"{e.FirstName} {e.LastName}"}
                ).ToList();

                return View(model);
            }

            var dto = ContractModelMapper.ToDto(model);
            var result = await _contractService.AddContract(dto);

            if (result != true)
            {
                ModelState.AddModelError("", "Failed to create contract.");
                return View(model);
            }

            TempData["SuccessMessage"] = "Contract created successfully.";
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var contractDto = await _contractService.GetContractByid(id);
            if (contractDto == null) 
            { 
                return NotFound();
            }

            var employees = await _employeeService.GetAllEmployees();

            var model = ContractModelMapper.ToEditModel(contractDto, employees);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ContractCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                var employees = await _employeeService.GetAllEmployees();
                model.Employees = employees.Select(e => new SelectListItem
                {
                    Value = e.EmployeeId.ToString(),
                    Text = $"{e.FirstName} {e.LastName}"
                }).ToList();

                return View(model);
            }

            var dto = ContractModelMapper.ToDtoForUpdate(model);
            var updated = await _contractService.UpdateContract(dto);

            if (updated != true)
            {
                ModelState.AddModelError("", "Failed to update contract.");
                return View();
            }

            TempData["SuccessMessage"] = "Contract updated successfully.";
            return RedirectToAction(nameof(Index));
        }


    }
}
