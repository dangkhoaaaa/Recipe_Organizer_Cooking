using System;
using System.Collections.Generic;

namespace RecipeOrganizer.Models
{
    public partial class Category
    {
        public int CategoryId { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ParentCategory Parent { get; set; } = null!;
    }
}
