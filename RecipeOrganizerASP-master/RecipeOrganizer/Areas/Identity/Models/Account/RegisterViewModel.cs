// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeOrganizer.Areas.Identity.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Must input {0}")]
        [EmailAddress(ErrorMessage = "Wrong Email format")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Must input {0}")]
        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Confirm password and password do not match")]
        public string ConfirmPassword { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Must input {0}")]
        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string UserName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        //[Required(ErrorMessage = "Must input {0}")]
       // [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 1)]
        public string? FirstName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
     //   [Required(ErrorMessage = "Must input {0}")]
      //  [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 1)]
        public string? LastName { get; set; }

    }
}
