using System.ComponentModel.DataAnnotations;

namespace HRManagementSystem.Data.Entities
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(250)]
        public string? Description { get; set; }

        // Navigation Property
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
