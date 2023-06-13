using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Data;
using Services.Models;
using Services.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Services.Repository
{
	public class RecipeRepository : RepositoryBase<Recipe>
	{
		Recipe_OrganizerContext _context;

		protected DbSet<Recipe> _dbSet;

		public RecipeRepository()
		{
			_context = new Recipe_OrganizerContext();
			_dbSet = _context.Set<Recipe>();
		}

<<<<<<< HEAD
	//	public  ICollection<Recipe> Products { get; set; } = new List<Recipe>();
=======

		public  ICollection<Recipe> Products { get; set; } = new List<Recipe>();

>>>>>>> main
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


		public Recipe GetById(int id)
		{
			return _dbSet.Where(r => r.RecipeId == id).FirstOrDefault();
		}


		public List<Recipe> getRecipeByKeywordWitPaging(string keyword,int productPage , int PageSize,List<Recipe> recipeAllSearch)

		{
			//var list = _dbSet.Where(Entity => Entity.Title.Contains(keyword)).ToList();
			List<Recipe> listRecipe = new List<Recipe>();
			int i = 1;
			foreach (Recipe recipe in recipeAllSearch)
			{

				if (recipe.Title.Contains(keyword + "") && i > ((productPage - 1) * PageSize) && i <= ((productPage - 1) * PageSize) + PageSize) { listRecipe.Add(recipe); }
				i++;
			}
			// return _dbSet.Where(p => p.Title.Contains(keyword)).ToList();
			return listRecipe;
		}

		public List<Recipe> getAllRecipe()
		{
			//var list = _dbSet.Where(Entity => Entity.Title.Contains(keyword)).ToList();
			List<Recipe> listRecipe = new List<Recipe>();
			foreach (Recipe recipe in _dbSet)
			{
				listRecipe.Add(recipe);

			}
			// return _dbSet.Where(p => p.Title.Contains(keyword)).ToList();
			return listRecipe;
		}

		public List<Recipe> getPaingRecipe(int productPage, int PageSize)
		{

			//var list = _dbSet.Where(Entity => Entity.Title.Contains(keyword)).ToList();
			List<Recipe> listRecipe = new List<Recipe>();


			int i = 1;
			foreach (Recipe recipe in _dbSet)
			{
				if (i > ((productPage - 1) * PageSize) && i <= ((productPage - 1) * PageSize) + PageSize)
				{
					listRecipe.Add(recipe);

				}

				i++;
			}
			// return _dbSet.Where(p => p.Title.Contains(keyword)).ToList();
			return listRecipe;
		}

	}
}


		public List<Recipe> getAllRecipe()
        {
            //var list = _dbSet.Where(Entity => Entity.Title.Contains(keyword)).ToList();
            List<Recipe> listRecipe = new List<Recipe>();
            foreach (Recipe recipe in _dbSet)
            {
                 listRecipe.Add(recipe);

            }
            // return _dbSet.Where(p => p.Title.Contains(keyword)).ToList();
            return listRecipe;
        }

        public List<Recipe> getPaingRecipe(int productPage , int PageSize)
        {
           
            //var list = _dbSet.Where(Entity => Entity.Title.Contains(keyword)).ToList();
            List<Recipe> listRecipe = new List<Recipe>();

            
            int i = 1;
            foreach (Recipe recipe in _dbSet)
            {
                if (i > ((productPage - 1) * PageSize) && i <= ((productPage - 1) * PageSize)+ PageSize) 
                {
                    listRecipe.Add(recipe);
                    
                }
                i++;
            }
            // return _dbSet.Where(p => p.Title.Contains(keyword)).ToList();
            return listRecipe;
        }

    }
	
}

