using Microsoft.AspNetCore.Identity;
using Services.Models.Authentication;

namespace RecipeOrganizer.Areas.Admin.Models.Manage
{
    public class IndexViewModel
    {
        public AppUser Member { get; set; }
        public List<string> Role { get; set; }
        public List<UserLoginInfo>? ExternalLogin { get; set; }
        public int TotalRecipe { get; set; }
        public bool Status { get; set; }
    }
}
