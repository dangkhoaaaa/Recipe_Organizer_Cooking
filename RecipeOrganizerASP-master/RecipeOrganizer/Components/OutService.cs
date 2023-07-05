using Microsoft.AspNetCore.Mvc;

namespace RecipeOrganizer.Components
{
	public class OutService : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
