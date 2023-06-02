using System.ComponentModel.DataAnnotations;

namespace RecipeOrganizer.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string RoleName { get; set; }

    }
}
