using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Repositories.Interfaces;
using HRManagementSystem.Services.Interfaces;
using HRManagementSystem.Services.Mappers;

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _attendanceRepository;

    public AttendanceService(IAttendanceRepository attendanceRepository)
    {
        _attendanceRepository = attendanceRepository;
    }


    public async Task<List<AttendanceDto>> GetAttendancesByDateRangAsync(DateTime startDate,DateTime endDate)
    {
        return await _attendanceRepository.GetAttendancesByDateRange(startDate, endDate);
    }

    public async Task<List<AttendanceDto>> GetAttendanceByEmployeeIdAsync(int employeeId)
    {
        if (employeeId <= 0)
        {
            throw new ArgumentException("Invalid employee id");
        }

        return await _attendanceRepository.GetAttendanceByEmployeeId(employeeId);
    }




    //  CHECK-IN / CHECK-OUT 
    public async Task<AttendanceDto> AddAttendanceAsync(AttendanceDto attendance)
    {
        if (attendance == null)
        {
            throw new ArgumentNullException(nameof(attendance));
        }

        if (attendance.EmployeeId <= 0)
        {
            throw new ArgumentException("Invalid employee id");
        }

        var entity = AttendanceMapper.ToEntity(attendance);

        bool hasCheckedIn = await _attendanceRepository.CheckAttendance(entity.EmployeeId, entity.AttendanceDate);

        if (hasCheckedIn)
        {
            // Check-out
            entity.CheckOutTime ??= DateTime.Now;
            await _attendanceRepository.UpdateAttendance(entity);
        }
        else
        {
            // Check-in
            entity.CheckInTime ??= DateTime.Now;
            await _attendanceRepository.AddAttendance(entity);
        }

        return attendance;
    }


    public async Task<AttendanceSummaryDto> GetDailySummaryAsync(DateTime date)
    {
        return await _attendanceRepository.GetDailySummary(date);
    }

    public async Task<int> GetMissingAttendanceCountAsync(DateTime date)
    {
        return await _attendanceRepository.GetMissingAttendanceCount(date);
    }
}
