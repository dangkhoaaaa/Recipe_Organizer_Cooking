using System.ComponentModel.DataAnnotations;

namespace RecipeOrganizer.Areas.Identity.Models.RoleViewModels
{
  public class CreateRoleModel
    {
        [Display(Name = "Role")]
        [Required(ErrorMessage = "Must input {0}")]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "{0} phải dài {2} đến {1} ký tự")]
        public string Name { get; set; }


    }
}
