using Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace Services.Repository
{
	public class RecipeHasTagRepository : RepositoryBase<RecipeHasTag>
	{
		public List<Tag> GetTagsByRecipeId(int recipeId)
		{
			var tags = _dbSet
				.Where(rht => rht.RecipeId == recipeId)
				.Select(rht => rht.Tag)
				.ToList();

			return tags;
		}

		public void AddByRecipeId(int recipeId, int tagId)
		{
			RecipeHasTag recipeHasTag = new RecipeHasTag
			{
				RecipeId = recipeId,
				TagId = tagId
			};
			_dbSet.Add(recipeHasTag);
			_context.SaveChanges();
		}

		public void DeleteByRecipeId(int recipeId)
		{
			var recipeHasTags = _dbSet.Where(r => r.RecipeId == recipeId);
			_dbSet.RemoveRange(recipeHasTags);
			_context.SaveChanges();
		}

		public List<Recipe> GetRecipesByTags(List<string> tags)
		{
			List<Recipe> recipes = _dbSet
				.Where(rht => tags.Contains(rht.Tag.TagName))
				.Select(rht => rht.Recipe)
				.ToList();

			return recipes;
		}

	}
}
