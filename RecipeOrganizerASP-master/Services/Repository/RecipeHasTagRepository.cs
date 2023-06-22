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
	}
}
