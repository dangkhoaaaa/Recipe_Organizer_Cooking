using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RecipeOrganizer.Areas.Admin.Models.Manage
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Must input {0}")]
        [Display(Name = "Username or email")]
        public string UserNameOrEmail { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Rememeber me")]
        public bool RememberMe { get; set; }
    }
}
