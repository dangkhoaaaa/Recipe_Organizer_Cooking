using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Security.Certificates;
using Services.Models;
using Services.Repository;
using System.Drawing.Printing;

namespace RecipeOrganizer.Components
{
	public class Search : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
