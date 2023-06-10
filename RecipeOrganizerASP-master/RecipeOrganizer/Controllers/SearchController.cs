using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Models;
using Services.Repository;

namespace RecipeOrganizer.Controllers
{
    public class SearchController : Controller
    {

		private readonly RecipeRepository _recipeRepository;

        public SearchController(RecipeRepository context)
       {
            _recipeRepository = context;  
        }

        // GET: Search/SearchKeyWord
        //public actionresult searchkeyword()
        //      {
        //          return view("search");
        //      }

        public IActionResult SearchKeyWord(string keyword)
		{
			keyword = "d";
			//var results = _recipeRepository.SearchByProperty(r => r.Title.Contains(keyword));
			var results = _recipeRepository.getRecipeByKeyword(keyword);
			return View("Search", results);
		}
	}
}
