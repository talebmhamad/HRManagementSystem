using System.ComponentModel.DataAnnotations;


namespace HRManagementSystem.Data.DTOs
{
    public class AttendanceDto
    {
        public int AttendanceId { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        [Required]
        public DateTime AttendanceDate { get; set; }

        public DateTime? CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = null!;
    }
}
