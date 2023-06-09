using Microsoft.AspNetCore.Identity;
using Services.Models.Authentication;
using System;
using System.Collections.Generic;

namespace Services.Models
{
    public partial class Collection
    {
        public string UserId { get; set; }
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;
        public virtual AppUser User { get; set; } = null!;
    }
}
