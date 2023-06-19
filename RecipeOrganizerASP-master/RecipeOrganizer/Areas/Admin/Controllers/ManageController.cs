﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipeOrganizer.Areas.Data;
using RecipeOrganizer.Areas.Admin.Models;
using Services.Models;
using Services.Models.Authentication;
using System;
using static RecipeOrganizer.Areas.Identity.Controllers.ManageController;
using Services.Repository;

namespace RecipeOrganizer.Areas.Admin.Controllers
{
    [Authorize]
	[Area("Admin")]
	[Route("/Admin/[action]")]
	public class ManageController : Controller
	{
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IEmailSender _emailSender;
        private readonly ILogger<ManageController> _logger;

        private UserRepository _userRepository;
        private RecipeRepository _recipeRepository;


        public ManageController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IEmailSender emailSender,
        ILogger<ManageController> logger        
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _logger = logger;
            _recipeRepository = new RecipeRepository();
            _userRepository = new UserRepository();
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            AddLoginSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        [HttpGet("/manage/")]
        public async Task<IActionResult> Index(ManageMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == ManageMessageId.ChangePasswordSuccess ? "Password has change"
                : message == ManageMessageId.SetPasswordSuccess ? "Password has reset"
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "Error"
                : message == ManageMessageId.AddPhoneSuccess ? "Phone number has added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Phone number has removed."
                : "";

            var listUser = _userRepository.GetAll();
            List<IndexViewModel> list = new List<IndexViewModel>();

            foreach (var user in listUser)
            {
                var userName = await _userManager.FindByIdAsync(user.Id);
                var role = await _userManager.GetRolesAsync(userName);
                var model = new IndexViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = role.ToList(),
                    TotalRecipe = 2, 
                    Status = true
                };
                list.Add(model);
            }
			
            return View(list);
        }

        private Task<AppUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        public async Task<IActionResult> SeedDataAsync()
        {
            // Create Roles
            var rolenames = typeof(RoleName).GetFields().ToList();
            foreach (var r in rolenames)
            {
                var rolename = (string)r.GetRawConstantValue();
                var rfound = await _roleManager.FindByNameAsync(rolename);
                if (rfound == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(rolename));
                }
            }
            // admin, pass=admin123, admin@example.com
            var useradmin = await _userManager.FindByEmailAsync("recipeorganizert3@gmail.com");
            if (useradmin == null)
            {
                useradmin = new AppUser()
                {
                    UserName = "admin",
                    Email = "recipeorganizert3@gmail.com",
                    EmailConfirmed = true,
                };
                await _userManager.CreateAsync(useradmin, "admin123");
                await _userManager.AddToRoleAsync(useradmin, RoleName.Administrator);
                await _signInManager.SignInAsync(useradmin, false);

                return RedirectToAction("SeedData");
            }
            else
            {
                var user = await _userManager.GetUserAsync(this.User);
                if (user == null) return this.Forbid();
                var roles = await _userManager.GetRolesAsync(user);

                if (!roles.Any(r => r == RoleName.Administrator))
                {
                    return this.Forbid();
                }
            }
            //SeedPostCategory();
            //SeedProductCategory();

            //StatusMessage = "Vừa seed Database";
            return RedirectToAction("Index");
        }
    }

}
