using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Data.Entities;

namespace HRManagementSystem.Services.Mappers
{
    public static class AttendanceMapper
    {
        
        public static Attendance ToEntity(AttendanceDto attendanceDto)
        {
            return new Attendance
            {
                AttendanceDate = attendanceDto.AttendanceDate,
                AttendanceId = attendanceDto.AttendanceId,
                CheckInTime = attendanceDto.CheckInTime,
                CheckOutTime = attendanceDto.CheckOutTime,
                EmployeeId = attendanceDto.EmployeeId,
                Status = attendanceDto.Status,
            };
        }

        public static AttendanceDto toDto(Attendance entity)
        {
            return new AttendanceDto
            {
                AttendanceDate = entity.AttendanceDate,
                CheckOutTime = entity.CheckOutTime,
                EmployeeId = entity.EmployeeId,
                Status = entity.Status,
                CheckInTime = entity.CheckInTime,

            };
        }
    }
}
