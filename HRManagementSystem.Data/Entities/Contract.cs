using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public bool IsActive { get; set; } = true!;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        public Employee Employee { get; set; } = null!;

    }
}
