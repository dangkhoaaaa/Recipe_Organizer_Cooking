using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipeOrganizer.Areas.Data;
using Services.Models;
using Services.Models.Authentication;
using System;

namespace RecipeOrganizer.Areas.Admin.Controllers
{
    [Authorize]
	[Area("Admin")]
	[Route("/Admin/[action]")]
	public class ManageController : Controller
	{
		private readonly Recipe_OrganizerContext _dbContext;
		private readonly UserManager<AppUser> _userManager;

		private readonly SignInManager<AppUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public ManageController(Recipe_OrganizerContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
		{
			_dbContext = dbContext;
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
		}

        [HttpGet("/manage/")]
        public IActionResult Index()
		{
			return View();
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
