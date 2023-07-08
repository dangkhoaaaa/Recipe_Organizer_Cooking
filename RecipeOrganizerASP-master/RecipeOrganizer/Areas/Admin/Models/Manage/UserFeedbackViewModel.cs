using Services.Models.Authentication;
using Services.Models;

namespace RecipeOrganizer.Areas.Admin.Models.Manage
{
    public class UserFeedbackViewModel
    {
        public AppUser User { get; set; }
        public List<Feedback> UserFeedback { get; set; }
        public int TotalFeedback { get; set; }
    }
}
