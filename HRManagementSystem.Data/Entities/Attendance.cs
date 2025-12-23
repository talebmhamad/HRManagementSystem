using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRManagementSystem.Data.Entities
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime AttendanceDate { get; set; }

        public DateTime? CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = null!;

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; } = null!;
    }
}
