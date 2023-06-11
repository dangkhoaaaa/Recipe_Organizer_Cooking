using Microsoft.EntityFrameworkCore;
using Services.Data;
using Services.Models;
using Services.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
	public class RecipeRepository : RepositoryBase<Recipe>
	{
		Recipe_OrganizerContext _context;
		DbSet<Recipe> _dbSet;
		public RecipeRepository()
		{
			_context = new Recipe_OrganizerContext();
			_dbSet = _context.Set<Recipe>();
		}

		public  ICollection<Recipe> Products { get; set; } = new List<Recipe>();
		public List<Recipe> getRecipeByKeyword(string keyword)
		{
			//var list = _dbSet.Where(Entity => Entity.Title.Contains(keyword)).ToList();
			List<Recipe> listRecipe = new List<Recipe>();
			foreach (Recipe recipe in _dbSet)
			{
				if (recipe.Title.Contains(keyword+"")){ listRecipe.Add(recipe); }
				
			}
			// return _dbSet.Where(p => p.Title.Contains(keyword)).ToList();
			return listRecipe;
		}
	}
	
}
