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
		private readonly CategoryRepository _categoryRepository;
		private readonly UserManager<AppUser> _userManager;
		private readonly CollectionRepository _collectionRepository;

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
			_categoryRepository = new CategoryRepository();
			_userManager = userManager;
			_collectionRepository = new CollectionRepository();
		}
		public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> Display(int productPage = 1)
		{
			// lay tat ca list recipe de dem so luong
			List<Recipe> recipes = _recipeRepository.getAllRecipe();

			List<Recipe> results = _recipeRepository.getPaingRecipe(productPage, PageSize, recipes);

			var user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				var checkRecipeSave = _collectionRepository.CollectionList(recipes, user.Id);
				ViewBag.CheckCollectionSave = checkRecipeSave;
			}
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

		public async Task<IActionResult> SearchKeyWordFitler(string keyword, string filter, int productPage = 1)
		{
			ViewBag.Keyword = keyword;
			ViewBag.filter = filter;
			List<Recipe> results = null;

			List<Recipe> recipesSearchAll = _recipeRepository.SearchAllTitleWithFilter(filter, keyword);



			if (keyword != null && recipesSearchAll.Count() > 0)
			{
				results = _recipeRepository.getRecipeByKeywordWitPaging(keyword, productPage, PageSize, recipesSearchAll);
				
				var user = await _userManager.GetUserAsync(User);
				if (user != null)
				{
					var checkRecipeSave = _collectionRepository.CollectionList(recipesSearchAll, user.Id);
					ViewBag.CheckCollectionSave = checkRecipeSave;
				}
			}
			else
			{
				ViewBag.notfound = "Not Found Recipe";
			}


			return View("DisplayRecipe", new RecipeListDisplayWithPaging
			{
				Recipes = results
					,
				PagingInfo = new PagingInfo
				{
					ItemsPerPage = PageSize,
					CurrentPage = productPage,
					TotalItems = recipesSearchAll.Count()

				}
			});
		}

		public async Task<IActionResult> DisplayCategoryIngredient(int categoryId, int productPage = 1)
		{
			Category category = _categoryRepository.getInfCategory(categoryId);


			ViewBag.Category = category;
			// lay tat ca list recipe de dem so luong
			List<Recipe> recipes = _recipeHasCategoryRepository.getRecipeByCategoryID(categoryId);

			List<Recipe> results = _recipeRepository.getRecipeByCategoryWitPaging(productPage, PageSize, recipes);

			var user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				var checkRecipeSave = _collectionRepository.CollectionList(recipes, user.Id);
				ViewBag.CheckCollectionSave = checkRecipeSave;
			}

			return View("DisplayCategoryIngredient",
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

		public async Task<IActionResult> DisplayCategoryMeal(int categoryId, int productPage = 1)
		{
			ViewBag.categoryId = categoryId;

			Category category = _categoryRepository.getInfCategory(categoryId);


			ViewBag.Category = category;

			// lay tat ca list recipe de dem so luong
			List<Recipe> recipes = _recipeHasCategoryRepository.getRecipeByCategoryID(categoryId);

			List<Recipe> results = _recipeRepository.getRecipeByCategoryWitPaging(productPage, PageSize, recipes);

			var user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				var checkRecipeSave = _collectionRepository.CollectionList(recipes, user.Id);
				ViewBag.CheckCollectionSave = checkRecipeSave;
			}

			return View("DisplayCategoryMeal",
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

		public async Task<IActionResult> DisplayCategoryOccatision(int categoryId, int productPage = 1)
		{

			// lay tat ca list recipe de dem so luong
			Category category = _categoryRepository.getInfCategory(categoryId);
			ViewBag.categoryId = categoryId;

			ViewBag.Category = category;
			List<Recipe> recipes = _recipeHasCategoryRepository.getRecipeByCategoryID(categoryId);

			List<Recipe> results = _recipeRepository.getRecipeByCategoryWitPaging(productPage, PageSize, recipes);

			var user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				var checkRecipeSave = _collectionRepository.CollectionList(recipes, user.Id);
				ViewBag.CheckCollectionSave = checkRecipeSave;
			}

			return View("DisplayCategoryOccatision",
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
