using Services.Models;
using Services.Models.Authentication;

namespace RecipeOrganizer.Areas.Admin.Models.Manage
{
    public class UserRecipeViewModel
    {
        public AppUser User { get; set; }
        public List<Recipe> UserRecipe { get; set; }
        public int TotalRecipe { get; set; }
    }
}
