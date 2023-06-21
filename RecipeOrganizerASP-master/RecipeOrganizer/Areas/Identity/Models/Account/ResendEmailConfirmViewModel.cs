using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RecipeOrganizer.Areas.Identity.Models.AccountViewModels
{
	public class ResendEmailConfirmViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }
	}
}
