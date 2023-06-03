using System;
using System.Collections.Generic;

namespace RecipeOrganizer.Models
{
    public partial class Category
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

        public int CategoryId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
