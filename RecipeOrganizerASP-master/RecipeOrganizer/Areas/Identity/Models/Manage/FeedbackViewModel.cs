using System.ComponentModel.DataAnnotations;


namespace RecipeOrganizer.Areas.Identity.Models.ManageViewModels
{
    public class FeedbackViewModel
    {
        public int RecipeId { get; set; }

        [Required(ErrorMessage = "Please enter a title for your feedback.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter your feedback.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter your user name.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please rate this recipe.")]
        [Range(1, 5, ErrorMessage = "Please enter a rating between 1 and 5.")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "")]
        public bool Status { get; set; }

        
    }
}
