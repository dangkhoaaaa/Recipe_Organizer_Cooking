using System;
using System.Collections.Generic;

namespace RecipeOrganizer.Models
{
    public partial class ParentCategory
    {
        public ParentCategory()
        {
            Categories = new HashSet<Category>();
        }

        public int ParentId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
