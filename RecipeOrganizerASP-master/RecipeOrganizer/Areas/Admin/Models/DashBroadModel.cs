using Services.Models;

namespace RecipeOrganizer.Areas.Admin.Models
{
    public class DashBroadModel
    {
        public int? recipePending { get; set; }
        public int? recipeReject { get; set; }

        public int? recipePublic { get; set; }

        public int? recipeTrash { get; set; }

        public int? recipeDrash { get; set; }

        public int? totalAccount { get; set; }
        public int? totalRecipe { get; set; }

        public int? totalFeedback { get; set; }

        public int? totalCategory { get; set; }

        public int? totalView { get; set; }

        public List<Recipe>? Recipes { get; set; }

        public int? NumberRecipeofMonth1 { get; set; }

        public int? NumberRecipeofMonth2 { get; set; }

        public int? NumberRecipeofMonth3 { get; set; }

        public int? NumberRecipeofMonth4 { get; set; }

        public int? NumberRecipeofMonth5 { get; set; }

        public int? NumberRecipeofMonth6 { get; set; }

        public int? NumberRecipeofMonth7 { get; set; }

        public int? OldUser1_10 { get; set; }
        public int? OldUser10_20 { get; set; }
        public int? OldUser20_30 { get; set; }
        public int? OldUser30_40 { get; set; }
        public int? OldUser40_50 { get; set; }
        public int? OldUser50_60 { get; set; }
        public int? OldUser60_200 { get; set; }


    }
}
