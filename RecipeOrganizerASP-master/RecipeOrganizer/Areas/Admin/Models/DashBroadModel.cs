using Services.Models;

namespace RecipeOrganizer.Areas.Admin.Models
{
    public class DashBroadModel
    {
        public int? recipePending { get; set; }
        public int? recipeReject { get; set; }

        public int? recipePublic { get; set; }

        public int? totalAccount { get; set; }
        public int? totalRecipe { get; set; }

        public int? totalFeedback { get; set; }

        public int? totalCategory { get; set; }

        public int? totalView { get; set; }

        public List<Recipe>? Recipes { get; set; }

    }
}
