using Services.Models;
using Services.Models.Authentication;

namespace RecipeOrganizer.Areas.Admin.Models.RecipeManage
{
    public class RecipeViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public int RecipeId { get; set; }
        public string RecipeTitle { get; set; }
        public string? RecipeDescription { get; set; }
        public int? NumberShare { get; set; }
        public string RecipeImage { get; set;}
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }

    }
}
