using System;
using System.Collections.Generic;

namespace RecipeOrganizer.Models
{
    public partial class RecipeHasDirection
    {
        public int DirectionId { get; set; }
        public int RecipeId { get; set; }

        public virtual Direction Direction { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
