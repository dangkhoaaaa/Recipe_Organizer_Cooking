using Microsoft.EntityFrameworkCore;
using Services.Models;

namespace Services.Repository
{
	public class RecipeRepository : RepositoryBase<Recipe>
	{
		DbSet<Recipe> _dbSet;
		public List<Recipe> getRecipeByKeyword(string keyword)
		{
			var list = _dbSet.Where(Entity => Entity.Title.Contains(keyword)).ToList();
			return list;
		}

		public Recipe GetById(int id)
		{
			return _dbSet.Where(r => r.RecipeId == id).FirstOrDefault();
		}

	}

}
