using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace RecipeOrganizer.Areas.Identity.Models.ManageViewModels
{
    public class ManagePhoneNumberViewModel
    {
        public string PhoneNumber { get; set;}
        public bool PhoneNumberConfirm { get; set;}

    }
}
