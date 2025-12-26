using System.ComponentModel.DataAnnotations;

namespace HRManagementSystem.Web.Models
{
    public class UserEditViewModel
    {
        public int? UserId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        // Only used on create / reset
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
