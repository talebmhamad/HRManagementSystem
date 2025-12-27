using System.ComponentModel.DataAnnotations;

namespace HRManagementSystem.Web.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        [MaxLength(100)]
        public string Name { get; set; } 

        [MaxLength(250)]
        public string? Description { get; set; }

    }
}
