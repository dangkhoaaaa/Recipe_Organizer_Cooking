using Microsoft.AspNetCore.Mvc;
using Services.Data;
using Services.Models;
using Services.Repository;
using System.Drawing.Printing;

namespace RecipeOrganizer.Components
{
	public class RelatedRecipes : ViewComponent
	{
		public int PageSize = 8;
		private readonly RecipeRepository _recipeRepository;
		private readonly RecipeHasTagRepository _recipeHasTagRepository;

		public RelatedRecipes()
		{
			_recipeRepository = new RecipeRepository();
			_recipeHasTagRepository = new RecipeHasTagRepository();
		}

		public IViewComponentResult Invoke(int productPage = 1)
		{
			//List<string> tags;
			//List<Recipe> recipes = _recipeHasTagRepository.GetRecipesByTags(tags);
			//List<Recipe> results = _recipeRepository.getPaingRecipe(productPage, PageSize, recipes);
			//return View(
			//new RecipeListDisplayWithPaging
			//{
			//	Recipes = results
			//		,
			//	PagingInfo = new PagingInfo
			//	{
			//		ItemsPerPage = PageSize,
			//		CurrentPage = productPage,
			//		TotalItems = recipes.Count()

			//	}
			//}

			//	);
			return View();
		}
	}
}
