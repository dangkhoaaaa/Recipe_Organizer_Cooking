using Services.Models.Authentication;
using Services.Models;
using Services.Data;

namespace RecipeOrganizer.Areas.Admin.Models.Manage
{
    public class UserFeedbackViewModel
    {
        public AppUser User { get; set; }
        public List<UserFeedback> UserFeedback { get; set; }
        public int TotalFeedback { get; set; }
    }
}
