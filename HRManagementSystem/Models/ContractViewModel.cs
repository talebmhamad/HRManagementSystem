namespace HRManagementSystem.Web.Models
{
    public class ContractViewModel
    {
        public int ContractId { get; set; }
        public string? EmployeeName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal BaseSalary { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
