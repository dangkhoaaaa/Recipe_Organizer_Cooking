using Microsoft.EntityFrameworkCore;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
	public class CategoryRepository : RepositoryBase<Category>
	{

	
		Recipe_OrganizerContext _context;
		DbSet<Category> _dbSet;
		public CategoryRepository()
		{
			_context = new Recipe_OrganizerContext();
			_dbSet = _context.Set<Category>();
		}

		

		public List<Category> getListCategoryById(int categoryID)
		{
			//var list = _dbSet.Where(Entity => Entity.Title.Contains(keyword)).ToList();
			List<Category> listRecipe = new List<Category>();
			foreach (Category category in _dbSet)
			{
				if (category.CategoryId==categoryID) { listRecipe.Add(category); }

			}
			// return _dbSet.Where(p => p.Title.Contains(keyword)).ToList();
			return listRecipe;
		}
	}
}
