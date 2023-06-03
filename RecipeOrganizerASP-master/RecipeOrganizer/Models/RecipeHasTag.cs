using System;
using System.Collections.Generic;

namespace RecipeOrganizer.Models
{
    public partial class RecipeHasTag
    {
        public int RecipeId { get; set; }
        public int TagId { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;
        public virtual Tag Tag { get; set; } = null!;
    }
}
