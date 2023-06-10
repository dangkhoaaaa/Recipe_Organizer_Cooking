using Microsoft.AspNetCore.Mvc;

namespace RecipeOrganizer.Controllers
{
	public class ContactController : Controller
	{

		public IActionResult SendContact()
		{
			return View();
		}
	}
}
