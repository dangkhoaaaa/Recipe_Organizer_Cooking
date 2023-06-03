using System;
using System.Collections.Generic;

namespace RecipeOrganizer.Models
{
    public partial class RecipeHasIngredient
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
