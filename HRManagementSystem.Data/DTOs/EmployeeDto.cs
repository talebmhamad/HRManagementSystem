
namespace HRManagementSystem.Data.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
        public bool HasUserAccount { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = "";
        public string? ProfileImagePath { get; set; }
    }
}
