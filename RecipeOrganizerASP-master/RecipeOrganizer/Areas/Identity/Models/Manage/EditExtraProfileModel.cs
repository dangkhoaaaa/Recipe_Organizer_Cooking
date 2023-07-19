using Services.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeOrganizer.Areas.Identity.Models.ManageViewModels
{
  public class EditExtraProfileModel
  {
        public string Id { get; set; }

        [Display(Name = "Username")]
		[StringLength(100)]
		public string Username { get; set; }

		[Phone]
        [Display(Name = "Phone number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "First name")]
        [StringLength(100)]
        public string? FirstName { get; set; }

        [Display(Name = "Last name")]
        [StringLength(100)]
        public string? LastName { get; set; }


        [Display(Name = "Birthday")]
		[DataType(DataType.DateTime)]
		[CustomDateTimeValidation(1900)]
		public DateTime? Birthday { get; set; }

        [Display(Name = "Image")]
        public string? Img { get; set; }
    }
}