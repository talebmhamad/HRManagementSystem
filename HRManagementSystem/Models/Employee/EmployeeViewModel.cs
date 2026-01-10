namespace HRManagementSystem.Web.Models.Employee
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }

        public string? FirstName { get; set; } 

        public string? LastName { get; set; } 

        public string? Email { get; set; } 
        public string? phone { get; set; }

        public bool IsActive { get; set; }

        public bool HasUserAccount { get; set; }

        public string? Department { get; set; }

  
    }
}
