using Microsoft.AspNetCore.Mvc;

namespace RecipeOrganizer.Controllers
{
	public class RecipeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
