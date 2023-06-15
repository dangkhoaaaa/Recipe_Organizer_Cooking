using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeOrganizer.Areas.Identity.Models.ManageViewModels
{
  public class EditExtraProfileModel
  {
      [Display(Name = "Username")]
      public string UserName { get; set; }

      [Display(Name = "Email address")]
      public string UserEmail { get; set; }
      [Display(Name = "PhoneNumber")]
      public string PhoneNumber { get; set; }

      [Display(Name = "Address")]
      [StringLength(400)]
      public string HomeAdress { get; set; }


      [Display(Name = "Date")]
      public DateTime? BirthDay { get; set; }
  }
}