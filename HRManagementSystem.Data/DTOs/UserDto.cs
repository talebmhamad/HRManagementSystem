

namespace HRManagementSystem.Data.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }

        public int EmployeeId { get; set; }

        public string Username { get; set; } ="";

        public string PasswordHash { get; set; } = "";

        public string Role { get; set; } = "";

        public bool IsActive { get; set; }

    }
}
