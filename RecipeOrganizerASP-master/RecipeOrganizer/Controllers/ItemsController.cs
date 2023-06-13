using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Models.Authentication;
using Services.Models;
using Services.Repository;

namespace RecipeOrganizer.Controllers
{
    public class ItemsController : Controller
    {
        public int PageSize = 8;
        private readonly RecipeRepository _recipeRepository;
        private readonly IngredientRepository _ingredientRepository;
        private readonly DirectionRepository _directionRepository;
        private readonly RecipeHasTagRepository _recipeHasTagRepository;
        private readonly TagRepository _tagRepository;
        private readonly RecipeHasCategoryRepository _recipeHasCategoryRepository;
        private readonly MetadataRepository _metadataRepository;
        private readonly MediaRepository _mediaRepository;
        private readonly UserManager<AppUser> _userManager;

        public ItemsController(UserManager<AppUser> userManager)
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
        public async Task<IActionResult> Display(int productPage)
        {
            // lay tat ca list recipe de dem so luong
            List<Recipe> recipes = _recipeRepository.getAllRecipe();

            List<Recipe> results = _recipeRepository.getPaingRecipe(productPage, PageSize);
            return View("DisplayRecipe",
            new RecipeListDisplayWithPaging
            {
                Recipes = results
                    ,
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = productPage,
                    TotalItems = recipes.Count()

                }
            }

                );
        }
    }
}
