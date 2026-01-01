using HRManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IContractService _contractService;
        private readonly IAttendanceService _attendanceService;

        public HomeController(IEmployeeService employeeService, IDepartmentService departmentService, IContractService contractService, IAttendanceService attendanceService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _contractService = contractService;
            _attendanceService = attendanceService;
        }
        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;

            var attendanceSummary = await _attendanceService.GetDailySummaryAsync(today);

            var missingAttendance = await _attendanceService.GetMissingAttendanceCountAsync(today);

            var stats = new Dictionary<string, int>
            {
                { "TotalEmployees", await _employeeService.GetTotalEmployeeCount() },
                { "ActiveEmployees", await _employeeService.GetActiveEmployeeCount() },
                { "Departments", await _departmentService.CountDepartments() },
                { "ExpiringContracts", await _contractService.CountExpiringContracts() },

                // Attendance dashboard
                { "Present", attendanceSummary.PresentCount },
                { "Absent", attendanceSummary.AbsentCount },
                { "Late", attendanceSummary.LateCount },
                { "MissingAttendance", missingAttendance }
            };

            return View(stats);
        }

        public IActionResult Error()
        {
            return View();
        }
      
    }
}



    


