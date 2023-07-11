using Microsoft.EntityFrameworkCore;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Services.Repository
{
	public class RecipeHasCategoryRepository : RepositoryBase<RecipeHasCategory>
    {
        private new readonly Recipe_OrganizerContext _context;

        protected DbSet<RecipeHasCategory> _dbSetRecipeHasCategory;
        protected DbSet<Recipe> _dbSetRecipe;

        public RecipeHasCategoryRepository()
        {
            _context = new Recipe_OrganizerContext();
            _dbSetRecipeHasCategory = _context.Set<RecipeHasCategory>();
            _dbSetRecipe = _context.Set<Recipe>();
        }

        // public ICollection<Recipe> Products { get; set; } = new List<Recipe>();

        public List<Recipe> getRecipeByCategoryID(int categoryId)
        {
            var query = from rc in _dbSetRecipeHasCategory
                        join r in _dbSetRecipe on rc.RecipeId equals r.RecipeId
                        where rc.CategoryId == categoryId && r.Status.Equals("public")
                        select r;

            return query.ToList();
        }

		public List<Category> GetCategoryByRecipeId(int recipeId)
		{
			var categories = _dbSet
				.Where(rht => rht.RecipeId == recipeId)
				.Select(rht => rht.Category)
				.ToList();

			return categories;
		}

		public List<int> GetSelectedCategoryIds(int recipeId)
		{
			return _context.RecipeHasCategories
				.Where(rhc => rhc.RecipeId == recipeId)
				.Select(rhc => rhc.CategoryId)
				.ToList();
		}

		public void AddCategory(List<int> categoryIds, int recipeId)
		{
			foreach (int categoryId in categoryIds)
			{
				RecipeHasCategory recipeHasCategory = new RecipeHasCategory
				{
					RecipeId = recipeId,
					CategoryId = categoryId
				};
				_context.RecipeHasCategories.Add(recipeHasCategory);
			}
			_context.SaveChanges();
		}

		public void UpdateRecipeCategories(List<int> categoryIds, int recipeId)
		{
			// First, remove the existing categories for the recipe
			var existingCategories = _context.RecipeHasCategories.Where(rhc => rhc.RecipeId == recipeId);
			_context.RecipeHasCategories.RemoveRange(existingCategories);

			// Add the new categories
			foreach (int categoryId in categoryIds)
			{
				RecipeHasCategory recipeHasCategory = new RecipeHasCategory
				{
					RecipeId = recipeId,
					CategoryId = categoryId
				};
				_context.RecipeHasCategories.Add(recipeHasCategory);
			}

			_context.SaveChanges();
		}

		//public List<Recipe> getRecipeByCategoryID(int categoryId)
		//{
		//    //var list = _dbSet.Where(Entity => Entity.Title.Contains(keyword)).ToList();
		//    List<Recipe> listRecipe = new List<Recipe>();
		//    foreach (RecipeHasCategory recipeCategory in _dbSetRecipeHasCategory)
		//    {

		//        if (recipeCategory.CategoryId==categoryId) {


		//            foreach (Recipe recipe in _dbSetRecipe)
		//            {

		//                if (recipe.RecipeId==recipeCategory.RecipeId) {


		//                    listRecipe.Add(recipe); 
		//                }


		//            }
		//        }


		//    }


		//    // return _dbSet.Where(p => p.Title.Contains(keyword)).ToList();
		//    return listRecipe;
		//}

	}
}
