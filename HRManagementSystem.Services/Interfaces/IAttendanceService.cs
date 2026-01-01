using HRManagementSystem.Data.DTOs;


namespace HRManagementSystem.Services.Interfaces
{
    public interface IAttendanceService
    {
        Task<List<AttendanceDto>> GetAttendanceByEmployeeIdAsync( int employeeid);

        Task<AttendanceDto> AddAttendanceAsync(AttendanceDto attendance);
        Task<List<AttendanceDto>> GetAttendancesByDateRangAsync(DateTime startDate, DateTime endDate);

        Task<AttendanceSummaryDto> GetDailySummaryAsync(DateTime date);
        Task<int> GetMissingAttendanceCountAsync(DateTime date);

    }
}
