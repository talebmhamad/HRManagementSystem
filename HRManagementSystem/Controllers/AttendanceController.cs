using HRManagementSystem.Services.Interfaces;
using HRManagementSystem.Web.Mappers;
using HRManagementSystem.Web.Models.Attendance;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Web.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var from = startDate ?? DateTime.Today;
                var to = endDate ?? DateTime.Today;

                var dtoList = await _attendanceService.GetAttendancesByDateRangAsync(from, to);
                var model = dtoList.Select(AttendanceModelMapper.ToModel).ToList();

                ViewBag.StartDate = from.ToString("yyyy-MM-dd");
                ViewBag.EndDate = to.ToString("yyyy-MM-dd");

                return View(model);
            }
            catch (Exception )
            {
                ModelState.AddModelError("", "Unable to load attendance data");

                ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
                ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

                return View(new List<AttendanceViewModel>());
            }
        }


    }
}
