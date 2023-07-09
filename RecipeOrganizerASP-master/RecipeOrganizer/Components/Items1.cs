using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Models;
using Services.Models.Authentication;
using Services.Repository;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace RecipeOrganizer.Components
{
	public class Items1 : ViewComponent
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
		private readonly CategoryRepository _categoryRepository;

		public Items1(UserManager<AppUser> userManager)
		{
			_recipeRepository = new RecipeRepository();
			_ingredientRepository = new IngredientRepository();
			_directionRepository = new DirectionRepository();
			_recipeHasTagRepository = new RecipeHasTagRepository();
			_tagRepository = new TagRepository();
			_recipeHasCategoryRepository = new RecipeHasCategoryRepository();
			_metadataRepository = new MetadataRepository();
			_mediaRepository = new MediaRepository();
			_categoryRepository = new CategoryRepository();
			_userManager = userManager;
		}
		public IViewComponentResult Invoke(int productPage = 1)
		{
			// lay tat ca list recipe de dem so luong
			List<Recipe> recipes = _recipeRepository.getAllRecipe();

			List<Recipe> results = _recipeRepository.getPaingRecipe(productPage, PageSize, recipes);
			return View(
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
