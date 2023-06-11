using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeOrganizer.Data;
using Services.Models;
using Services.Models.Authentication;
using Services.Repository;

namespace RecipeOrganizer.Controllers
{
    public class SearchController : Controller
    {

		//      private readonly recipe_organizercontext _reciperepository;
		//private readonly reciperepository _reciperepository1;

		//public searchcontroller(usermanager<recipe> context)
		//      {
		//          _reciperepository = context;
		//      }

		private readonly RecipeRepository _recipeRepository;
		private readonly IngredientRepository _ingredientRepository;
		private readonly DirectionRepository _directionRepository;
		private readonly RecipeHasTagRepository _recipeHasTagRepository;
		private readonly TagRepository _tagRepository;
		private readonly RecipeHasCategoryRepository _recipeHasCategoryRepository;
		private readonly MetadataRepository _metadataRepository;
		private readonly MediaRepository _mediaRepository;
		private readonly UserManager<AppUser> _userManager;

		public SearchController(UserManager<AppUser> userManager)
		{
			_recipeRepository = new RecipeRepository();
			_ingredientRepository = new IngredientRepository();
			_directionRepository = new DirectionRepository();
			_recipeHasTagRepository = new RecipeHasTagRepository();
			_tagRepository = new TagRepository();
			_recipeHasCategoryRepository = new RecipeHasCategoryRepository();
			_metadataRepository = new MetadataRepository();
			_mediaRepository = new MediaRepository();
			_userManager = userManager;
		}
		public IActionResult Index()
        {
            return View();
        }

        // GET: Search/SearchKeyWord
        
        public IActionResult SearchKeyWord(string keyword)
        {
			ViewBag.Keyword = keyword;
            List<Recipe> results = _recipeRepository.getRecipeByKeyword(keyword);
            return View(results);
        }





        [HttpPost]
        public async Task<IActionResult> SearchKeyWord1()
        {
            //var results = _recipeRepository.getRecipeByKeyword(keyword);
            return View();
        }
         
	}
}
