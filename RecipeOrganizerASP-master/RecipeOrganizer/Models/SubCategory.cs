using System;
using System.Collections.Generic;

namespace RecipeOrganizer.Models
{
    public partial class SubCategory
    {
        public int SubcategoryId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public virtual Category Category { get; set; } = null!;
    }
}
