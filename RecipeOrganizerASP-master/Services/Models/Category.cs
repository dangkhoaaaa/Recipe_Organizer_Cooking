using System;
using System.Collections.Generic;

namespace Services.Models
{
    public partial class Category
    {
        public int CategoryId { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Image { get; set; }

        public virtual ParentCategory Parent { get; set; } = null!;
    }
}
