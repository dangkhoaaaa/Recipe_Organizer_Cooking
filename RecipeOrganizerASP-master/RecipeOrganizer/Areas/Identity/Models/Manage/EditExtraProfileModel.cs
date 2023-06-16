using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeOrganizer.Areas.Identity.Models.ManageViewModels
{
  public class EditExtraProfileModel
  {

		[Display(Name = "Username")]
		[StringLength(100)]
		public string Username { get; set; }

		[Phone]
        [Display(Name = "Phone number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Firstname")]
        [StringLength(100)]
        public string? FirstName { get; set; }

        [Display(Name = "Lastname")]
        [StringLength(100)]
        public string? LastName { get; set; }


        [Display(Name = "Birthday")]
        public DateTime? Birthday { get; set; }
    }
}