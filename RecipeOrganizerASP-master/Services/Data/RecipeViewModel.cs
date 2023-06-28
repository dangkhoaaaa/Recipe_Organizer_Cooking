using Services.Models;
using Services.Models.Authentication;

namespace Services.Data
{
    public class RecipeViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int RecipeId { get; set; }
        public string RecipeTitle { get; set; }
        public string? RecipeDescription { get; set; }
        public int? NumberShare { get; set; }
        public string RecipeImage { get; set;}
        public string UserImage { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }
        public double? AvgRate { get; set; } 
        public List<Ingredient> Ingredients { get; set; }
        public List<Direction> Directions { get; set; }
        public List<Tag> Tags { get; set; }

    }
}
