using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Web.Models.Attendance;

namespace HRManagementSystem.Web.Mappers
{
    public static class AttendanceModelMapper
    {
       
        public static AttendanceViewModel ToModel(AttendanceDto dto)
        {
            return new AttendanceViewModel
            {
                EmployeeName = dto.EmployeeName,
                AttendanceDate = dto.AttendanceDate,
                CheckInTime = dto.CheckInTime,
                CheckOutTime = dto.CheckOutTime,
                Status = dto.Status
            };
        }
    }

}
