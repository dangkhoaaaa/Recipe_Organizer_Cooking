using System;
using System.Collections.Generic;

namespace Services.Models
{
    public partial class Collection
    {
        public int CollectionId { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
