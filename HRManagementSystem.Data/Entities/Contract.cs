using System.ComponentModel.DataAnnotations;

namespace HRManagementSystem.Data.Entities
{
    public class Contract
    {
        [Key]
        public int ContractId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public decimal BaseSalary { get; set; }

        [Required]
        public string status { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string ContractType { get; set; } = null!;

        public Employee Employee { get; set; } = null!;
    }
}
