using System;
using System.Collections.Generic;

namespace RecipeOrganizer.Models
{
    public partial class Direction
    {
        public int DirectionId { get; set; }
        public int RecipeId { get; set; }
        public int Step { get; set; }
        public string Direction1 { get; set; } = null!;

        public virtual Recipe Recipe { get; set; } = null!;
    }
}
