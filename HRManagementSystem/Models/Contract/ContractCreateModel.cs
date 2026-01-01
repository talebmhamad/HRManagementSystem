using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HRManagementSystem.Web.Models
{
    public class ContractCreateModel
    {
        [Required]
        public int EmployeeId { get; set; }
        public int ContractId { get; set; }
        public List<SelectListItem> Employees { get; set; } = new();

        [Required]
        [DataType(DataType.Date)]

        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required]
        public decimal BaseSalary { get; set; }

        public bool IsActive { get; set; } = true;

    
    }
}
