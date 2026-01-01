using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Web.Controllers.Api
{
    [Route("api/attendance")]
    [ApiController]
    public class AttendanceApiController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceApiController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        //  GET attendance by employee
        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetByEmployee(int employeeId)
        {

            if (employeeId <= 0)
            {
                return BadRequest("Invalid employee id");
            }

            var result = await _attendanceService
                .GetAttendanceByEmployeeIdAsync(employeeId);

            return Ok(result);
        
        }

        //   check-in
        [HttpPost("check")]
        public async Task<IActionResult> CheckAttendance([FromBody] AttendanceDto attendance)
        {
            if (attendance == null)
            {
                return BadRequest("Attendance data is required");
            }

            if (attendance.EmployeeId <= 0)
            {
                return BadRequest("Invalid employee id");
            }

            var result = await _attendanceService.AddAttendanceAsync(attendance);

            return Ok(result);
        
        }

        //   check-out
        [HttpPut("checkout")]
        public async Task<IActionResult> CheckOut([FromBody] AttendanceDto attendance)
        {

            if (attendance == null)
            {
                return BadRequest("Attendance data is required");
            }

            if (attendance.EmployeeId <= 0)
            {
                return BadRequest("Invalid employee id");
            }

            var result = await _attendanceService.AddAttendanceAsync(attendance);

            return Ok(result);
        
        }
    }
}
