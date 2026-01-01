using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Data.Entities;

namespace HRManagementSystem.Repositories.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<List<AttendanceDto>> GetAttendanceByEmployeeId(int employeeId);
        Task<List<AttendanceDto>> GetAttendancesByDateRange(DateTime startDate, DateTime endDate);
        Task<bool> AddAttendance(Attendance attendance);
        Task<bool> CheckAttendance(int employeeId, DateTime date);
        Task<bool> UpdateAttendance(Attendance attendance);
        Task<AttendanceSummaryDto> GetDailySummary(DateTime date);
        Task<int> GetMissingAttendanceCount(DateTime date);
    
    }
}
