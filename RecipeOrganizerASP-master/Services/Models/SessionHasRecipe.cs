using System;
using System.Collections.Generic;

namespace Services.Models
{
    public partial class SessionHasRecipe
    {
        public int SessionId { get; set; }
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;
        public virtual Session Session { get; set; } = null!;
    }
}
