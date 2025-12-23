using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRManagementSystem.Data.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string PasswordHash { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; } = null!;
    }
}
