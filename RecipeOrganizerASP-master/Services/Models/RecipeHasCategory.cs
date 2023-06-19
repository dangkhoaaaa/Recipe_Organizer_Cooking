using System;
using System.Collections.Generic;

namespace Services.Models
{
    public partial class RecipeHasCategory
    {
        public int RecipeHasCategoryId { get; set; }
        public int CategoryId { get; set; }
        public int RecipeId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
