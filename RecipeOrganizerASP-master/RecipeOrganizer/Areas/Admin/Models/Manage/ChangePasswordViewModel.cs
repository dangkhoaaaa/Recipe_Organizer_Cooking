using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RecipeOrganizer.Areas.Admin.Models.Manage
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Must input {0}")]
        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Must input {0}")]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm email")]
        [Compare("NewPassword", ErrorMessage = "Confirmation password must match the new password")]
        public string ConfirmPassword { get; set; }
    }
}
