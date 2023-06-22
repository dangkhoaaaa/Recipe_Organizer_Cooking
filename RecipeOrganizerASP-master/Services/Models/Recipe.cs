using System;
using System.Collections.Generic;

namespace Services.Models
{
    public partial class Recipe
    {
        public int RecipeId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date { get; set; }
        public int NumberShare { get; set; }
        public string Status { get; set; } = null!;
        public double? AvgRate { get; set; }
        public string? Image { get; set; }

        public virtual ICollection<Direction> Directions { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<Metadata> MetaData { get; set; }
    }
}
