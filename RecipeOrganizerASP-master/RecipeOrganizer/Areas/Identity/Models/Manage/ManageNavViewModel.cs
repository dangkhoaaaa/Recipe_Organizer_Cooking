using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecipeOrganizer.Areas.Identity.Models.ManageViewModels
{
    public class ManageNavViewModel
    {
        public string Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }

        public bool HasPassword { get; set; }
    }
}


