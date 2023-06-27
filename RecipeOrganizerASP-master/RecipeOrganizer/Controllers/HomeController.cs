using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipeOrganizer.Areas.Data;
using Services.Models;
using System.Diagnostics;

namespace RecipeOrganizer.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly RoleManager<IdentityRole> _roleManager;

		public HomeController(RoleManager<IdentityRole> roleManager, ILogger<HomeController> logger)
		{
			_roleManager = roleManager;
			_logger = logger;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Error()
		{
			return View();
		}


		[HttpGet]
		public ActionResult Search()
		{
			return RedirectToAction("SearchKeyWord", "Search");
		}

		public ActionResult PageNotFound()
		{
			return View();
		}

		[AllowAnonymous]
		[Route("/active")]
		public async Task<IActionResult> Active()
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
			return RedirectToAction("Index");
		}


		// Add new Recipe
		[HttpGet]
		public ActionResult AddNewRecipe()
		{
			return RedirectToAction("AddNewRecipe", "Recipe");
		}

		public IActionResult Privacy()
		{
			return View();
		}


	}
}