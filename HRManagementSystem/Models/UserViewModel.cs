using System.ComponentModel.DataAnnotations;

namespace HRManagementSystem.Web.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Role { get; set; } = null!;
        public bool IsActive { get; set; }
        public string EmployeeName { get; set; } = null!;
    }
}
