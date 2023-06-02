
namespace RecipeOrganizer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class User_Like_Recipe
    {
        [Key]
        public int id { get; set; }
        public int recipe_id { get; set; }
        public int user_id { get; set; }
    
        public virtual Recipe Recipe { get; set; }
        public virtual User User { get; set; }
    }
}
