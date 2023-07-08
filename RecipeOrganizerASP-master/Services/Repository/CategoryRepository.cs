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
		protected DbSet<Category> _dbSet1;
		public CategoryRepository()
		{
			_context = new Recipe_OrganizerContext();
			_dbSet1 = _context.Set<Category>();
		}

		public ICollection<Category> Products { get; set; } = new List<Category>();

		public List<Category> getListCategoryById(int categoryID)
		{
			//var list = _dbSet.Where(Entity => Entity.Title.Contains(keyword)).ToList();
			List<Category> listRecipe = new List<Category>();
			int count = 0;
			foreach (Category category in _dbSet1)
			{
				if ((category.ParentId == categoryID)&&(count<10)) { listRecipe.Add(category);
					count++;
				}
			}
			// return _dbSet.Where(p => p.Title.Contains(keyword)).ToList();
			return listRecipe;
		}

        public List<Category> getListCategoryAll()
        {
            //var list = _dbSet.Where(Entity => Entity.Title.Contains(keyword)).ToList();
            List<Category> listRecipe = new List<Category>();
            
            foreach (Category category in _dbSet1)
            {
                
                    listRecipe.Add(category);
                 
            }
            // return _dbSet.Where(p => p.Title.Contains(keyword)).ToList();
            return listRecipe;
        }



        public Category getInfCategory(int categoryID)
		{
			//var list = _dbSet.Where(Entity => Entity.Title.Contains(keyword)).ToList();
			List<Category> listRecipe = new List<Category>();
			Category result = new Category();
			foreach (Category category in _dbSet1)
			{
				if (category.CategoryId == categoryID)
				{
					result = category;
					break;
				}

			}
			// return _dbSet.Where(p => p.Title.Contains(keyword)).ToList();
			return result;
		}

		public List<Category> GetAllCategories ()
		{
			return _dbSet1.ToList();
		}
	}
}
