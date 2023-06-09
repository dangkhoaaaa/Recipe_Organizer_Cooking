using Microsoft.AspNetCore.Mvc;
using Services.Models;

namespace RecipeOrganizer.Controllers
{
    public class SearchController : Controller
    {
      //  private readonly Recipe _context;

        //public searchcontroller(recipe context)
        //{
        //    _context = context;
        //}

		// GET: Search/SearchKeyWord
		public ActionResult SearchKeyWord()
        {
            return View("Search");
        }
    }
}
