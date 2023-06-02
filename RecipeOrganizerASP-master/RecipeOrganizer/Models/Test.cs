using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeOrganizer.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Role")]    
        public int RoleID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }
        
    }
}
