using HRManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _DepartmentService;
        private readonly IContractService _contractService;

        public HomeController(IEmployeeService employeeService, IDepartmentService departmentService, IContractService ContractService )
        {
            _employeeService = employeeService;
            _DepartmentService = departmentService;
            _contractService = ContractService;
        }
        public async Task<ActionResult> Index()
        {
            Dictionary<string, int> stats = new Dictionary<string, int>
            {
                { "TotalEmployees", await _employeeService.GetTotalEmployeeCount() },
                { "ActiveEmployees", await _employeeService.GetActiveEmployeeCount() },
                { "Departments", await _DepartmentService.CountDepartments()},
                { "ExpiringContracts", await _contractService.CountExpiringContracts()},

            };
            return View(stats);
        }


    }

}
