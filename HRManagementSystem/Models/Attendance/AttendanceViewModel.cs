namespace HRManagementSystem.Web.Models.Attendance
{
    public class AttendanceViewModel
    {
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime AttendanceDate { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string Status { get; set; } = string.Empty;
    }

}
