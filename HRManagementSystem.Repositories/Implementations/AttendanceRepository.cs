using HRManagementSystem.Data;
using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Data.Entities;
using HRManagementSystem.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace HRManagementSystem.Repositories.Implementations
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly HRDbContext _context;
        private string connectionString;

        public AttendanceRepository(HRDbContext context, IConfiguration configuration)
        {
            _context = context;
            connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("DefaultConnection string is not configured.");
        }

        public async Task<List<AttendanceDto>> GetAttendanceByEmployeeId(int employeeId)
        {
            var list = new List<AttendanceDto>();

            try
            {
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand(
                    @"SELECT
                a.AttendanceId,
                a.EmployeeId,
                a.AttendanceDate,
                a.CheckInTime,
                a.CheckOutTime,
                a.Status,
                e.FirstName + ' ' + e.LastName AS EmployeeName
              FROM Attendances a
              INNER JOIN Employees e ON a.EmployeeId = e.EmployeeId
              WHERE a.EmployeeId = @EmployeeId",
                    connection);

                command.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = employeeId;

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    list.Add(new AttendanceDto
                    {
                        AttendanceId = reader.GetInt32(0),
                        EmployeeId = reader.GetInt32(1),
                        AttendanceDate = reader.GetDateTime(2),
                        CheckInTime = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
                        CheckOutTime = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
                        Status = reader.GetString(5),
                        EmployeeName = reader.GetString(6)
                    });
                }

                return list;
            }
            catch (SqlException)
            {
                throw;
            }
        
        }


        public async Task<bool> AddAttendance(Attendance attendance)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand(
                    @"INSERT INTO Attendances (EmployeeId,AttendanceDate, CheckIn, CheckOut, Status)
          VALUES (@EmployeeId,@AttendanceDate, @CheckIn, @CheckOut, @Status)",
                    connection);

                command.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = attendance.EmployeeId;
                command.Parameters.Add("@CheckIn", SqlDbType.DateTime).Value = (object?)attendance.CheckInTime ?? DBNull.Value;
                command.Parameters.Add("@CheckOut", SqlDbType.DateTime).Value = (object?)attendance.CheckOutTime ?? DBNull.Value;
                command.Parameters.Add("@Status", SqlDbType.NVarChar, 20).Value = attendance.Status;
                command.Parameters.Add("@AttendanceDate", SqlDbType.Date).Value = attendance.AttendanceDate.Date;

                return await command.ExecuteNonQueryAsync() > 0;
            }
            catch (SqlException)
            {
                throw;
            }

        }

        public async Task<List<AttendanceDto>> GetAttendancesByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var attendances = new List<AttendanceDto>();
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                using var command = new SqlCommand(
                    @"SELECT 
    a.AttendanceId,
    a.EmployeeId,
    a.AttendanceDate,
    a.CheckInTime,
    a.CheckOutTime,
    a.Status,
    e.FirstName + ' ' + e.LastName AS EmployeeName
FROM Attendances a
INNER JOIN Employees e 
    ON a.EmployeeId = e.EmployeeId
          WHERE Date BETWEEN @StartDate AND @EndDate",
                    connection);
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    attendances.Add(new AttendanceDto
                    {
                        AttendanceId = reader.GetInt32(0),
                        EmployeeId = reader.GetInt32(1),
                        AttendanceDate = reader.GetDateTime(2),
                        CheckInTime = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
                        CheckOutTime = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
                        Status = reader.GetString(5),
                        EmployeeName = reader.GetString(6),
                    });
                }
                return attendances;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task<bool> CheckAttendance(int employeeId, DateTime date)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand(
                    @"SELECT COUNT(1)
          FROM Attendances
          WHERE EmployeeId = @EmployeeId
            AND CAST([Date] AS date) = @Date
            AND Status = @Status",
                    connection);

                command.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = employeeId;
                command.Parameters.Add("@Date", SqlDbType.Date).Value = date.Date;
                command.Parameters.Add("@Status", SqlDbType.NVarChar).Value = "CheckIn";

                var count = Convert.ToInt16(await command.ExecuteScalarAsync());
                return count > 0;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAttendance(Attendance attendance)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand(
                    @"UPDATE Attendances
          SET CheckOut = @CheckOut,
              Status = @Status
          WHERE EmployeeId = @EmployeeId
            AND CAST([Date] AS date) = @Date
            AND CheckOut IS NULL",
                    connection);

                command.Parameters.AddWithValue("@EmployeeId", attendance.EmployeeId);

                command.Parameters.AddWithValue("@Date", attendance.AttendanceDate);

                command.Parameters.AddWithValue("@CheckOut", attendance.CheckOutTime);

                command.Parameters.AddWithValue("@Status", "CheckOut");

                var rows = await command.ExecuteNonQueryAsync();
                return rows > 0;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task<AttendanceSummaryDto> GetDailySummary(DateTime date)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand(
                    @"SELECT
            SUM(CASE WHEN Status = 'Present' THEN 1 ELSE 0 END),
            SUM(CASE WHEN Status = 'Absent'  THEN 1 ELSE 0 END),
            SUM(CASE WHEN Status = 'Late'    THEN 1 ELSE 0 END)
          FROM Attendances
          WHERE CAST(AttendanceDate AS date) = @Date",
                    connection);

                command.Parameters.Add("@Date", SqlDbType.Date).Value = date.Date;

                using var reader = await command.ExecuteReaderAsync();
                await reader.ReadAsync();

                return new AttendanceSummaryDto
                {
                    PresentCount = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                    AbsentCount = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                    LateCount = reader.IsDBNull(2) ? 0 : reader.GetInt32(2)
                };
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task<int> GetMissingAttendanceCount(DateTime date)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand(
                    @"SELECT COUNT(1)
          FROM Employees e
          WHERE e.IsActive = 1
            AND NOT EXISTS (
                SELECT 1
                FROM Attendances a
                WHERE a.EmployeeId = e.EmployeeId
                  AND CAST(a.AttendanceDate AS date) = @Date
            )",
                    connection);

                command.Parameters.Add("@Date", SqlDbType.Date).Value = date.Date;

                return Convert.ToInt16(await command.ExecuteScalarAsync());
            }
            catch (SqlException)
            {
                throw;
            }
        }


    }

}
