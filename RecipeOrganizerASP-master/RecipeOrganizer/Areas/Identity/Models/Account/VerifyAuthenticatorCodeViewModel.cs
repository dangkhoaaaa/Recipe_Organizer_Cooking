// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;

namespace RecipeOrganizer.Areas.Identity.Models.AccountViewModels
{
    public class VerifyAuthenticatorCodeViewModel
    {
        [Required(ErrorMessage = "Must enter {0}")]
        [Display(Name = "Enter the saved recovery code")]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Remember brower")]
        public bool RememberBrowser { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
