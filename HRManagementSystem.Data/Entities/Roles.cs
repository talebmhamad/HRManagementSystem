using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRManagementSystem.Data.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
