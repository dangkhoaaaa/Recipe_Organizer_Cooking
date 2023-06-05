using System;
using System.Collections.Generic;

namespace RecipeOrganizer.Models
{
    public partial class Ingredient
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = null!;
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;
    }
}
