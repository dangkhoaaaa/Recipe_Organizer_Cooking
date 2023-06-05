using System;
using System.Collections.Generic;

namespace RecipeOrganizer.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            Collections = new HashSet<Collection>();
            Directions = new HashSet<Direction>();
            Ingredients = new HashSet<Ingredient>();
            MetaData = new HashSet<Metadata>();
        }

        public int RecipeId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date { get; set; }
        public int NumberShare { get; set; }
        public string Status { get; set; } = null!;

        public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<Direction> Directions { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<Metadata> MetaData { get; set; }
    }
}
