using Microsoft.EntityFrameworkCore;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	}
	
}
