﻿using Firebase.Auth;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Services.Repository
{
	public class RecipeRepository : RepositoryBase<Recipe>
	{
		Recipe_OrganizerContext _context;

		protected DbSet<Recipe> _dbSet;
		protected DbSet<RecipeHasCategory> _dbSet1;
		protected DbSet<AppUser> _dbSetUser;
		protected DbSet<Metadata> _dbSetMetadata;
		public RecipeRepository()
		{
			_context = new Recipe_OrganizerContext();
			_dbSet = _context.Set<Recipe>();
			_dbSet1 = _context.Set<RecipeHasCategory>();
			_dbSetUser = _context.Set<AppUser>();
		}



		public ICollection<Recipe> Products { get; set; } = new List<Recipe>();


		public List<Recipe> getRecipeByKeyword(string keyword)
		{
			List<Recipe> listRecipe = new List<Recipe>();
			if (keyword != null && keyword.Length > 0 && keyword.Trim() != "")
			{
				//var list = _dbSet.Where(Entity => Entity.Title.Contains(keyword)).ToList();

				foreach (Recipe recipe in _dbSet)
				{
					if (recipe.Title.ToUpper().Contains(keyword.ToUpper()) && recipe.Status.Equals("public")) { listRecipe.Add(recipe); }
				}
			}
			// return _dbSet.Where(p => p.Title.Contains(keyword)).ToList();
			return listRecipe;
		}

		public List<String> getListTitleRecipeByKeyword(string keyword)
		{
			List<String> listRecipe = new List<String>();
			if (keyword != null && keyword.Length > 0 && keyword.Trim() != "")
			{
				//var list = _dbSet.Where(Entity => Entity.Title.Contains(keyword)).ToList();

				foreach (Recipe recipe in _dbSet)
				{
					if (recipe.Title.ToUpper().Contains(keyword.ToUpper()) && recipe.Status.Equals("public")) { listRecipe.Add(recipe.Title); }
				}
			}
			// return _dbSet.Where(p => p.Title.Contains(keyword)).ToList();
			return listRecipe;
		}


		public Recipe GetById(int id)
		{
			return _dbSet.Where(r => r.RecipeId == id && r.Status.Equals("public")).FirstOrDefault();
		}
		public Recipe GetByIdForEdit(int id)
		{
			return _dbSet.Where(r => r.RecipeId == id).FirstOrDefault();
		}

		public List<Recipe> GetByAuthor(string userId)
		{
			List<Recipe> listRecipe = _dbSet.Where(r => r.MetaData.Any(md => md.UserId == userId && md.FeedbackId == null))
										   .ToList();

			return listRecipe;
		}
		//public List<Recipe> GetUserCollection(string userId)
		//{
		//	List<Recipe> listRecipe = _dbSet.Where(r => r.Collection.Any(c => c.UserId == userId))
		//								   .ToList();

		//	return listRecipe;
		//}

		public List<Recipe> SearchAllTitleWithFilter(string filter, string title)
		{
			switch (filter)
			{
				case "1":
					return _dbSet.Where(r => r.Title.Contains(title) && r.Status.Equals("public")).OrderByDescending(r => r.NumberShare).ToList();
				case "2":
					return _dbSet.Where(r => r.Title.Contains(title) && r.Status.Equals("public")).OrderBy(r => r.Date).ToList();
				case "3":
					return _dbSet.Where(r => r.Title.Contains(title) && r.Status.Equals("public")).OrderBy(r => r.Title).ToList();
				case "4":
					return _dbSet.Where(r => r.Title.Contains(title) && r.Status.Equals("public")).OrderBy(r => r.RecipeId).ToList();
				default:
					return _dbSet.Where(r => r.Title.Contains(title) && r.Status.Equals("public")).OrderBy(r => r.Title).ToList();

			}

		}

		public List<Recipe> SearchAllTitleWithFilterandCategory(string filter, string title, int categoryID)
		{
			List<Recipe> listmapCategory = new List<Recipe>();
			var query = from rc in _dbSet1
						join r in _dbSet on rc.RecipeId equals r.RecipeId
						where rc.CategoryId == categoryID && r.Status.Equals("public", StringComparison.OrdinalIgnoreCase)
						select r;

			listmapCategory = query.ToList();
			switch (filter)
			{
				case "1":
					return listmapCategory.Where(r => r.Title.Contains(title)).OrderBy(r => r.NumberShare).ToList();
				case "2":
					return listmapCategory.Where(r => r.Title.Contains(title)).OrderBy(r => r.Date).ToList();
				case "3":
					return listmapCategory.Where(r => r.Title.Contains(title)).OrderBy(r => r.Title).ToList();
				case "4":
					return listmapCategory.Where(r => r.Title.Contains(title)).OrderBy(r => r.Ingredients).ToList();
				default:
					return listmapCategory.Where(r => r.Title.Contains(title)).OrderBy(r => r.Title).ToList();

			}

		}

		public List<Recipe> getRecipeByKeywordWitPaging(string keyword, int productPage, int PageSize, List<Recipe> recipeAllSearch)

		{
			//var list = _dbSet.Where(Entity => Entity.Title.Contains(keyword)).ToList();
			List<Recipe> listRecipe = new List<Recipe>();
			int i = 1;
			foreach (Recipe recipe in recipeAllSearch)
			{

				if (recipe.Title.ToUpper().Contains(keyword.ToUpper()) && recipe.Status.Equals("public", StringComparison.OrdinalIgnoreCase) && i > ((productPage - 1) * PageSize) && i <= ((productPage - 1) * PageSize) + PageSize) { listRecipe.Add(recipe); }
				i++;
			}
			// return _dbSet.Where(p => p.Title.Contains(keyword)).ToList();
			return listRecipe;
		}

		public List<Recipe> getRecipeByCategoryWitPaging(int productPage, int PageSize, List<Recipe> recipeAllSearch)

		{
			//var list = _dbSet.Where(Entity => Entity.Title.Contains(keyword)).ToList();
			List<Recipe> listRecipe = new List<Recipe>();
			int i = 1;
			foreach (Recipe recipe in recipeAllSearch)
			{

				if (recipe.Status.Equals("public", StringComparison.OrdinalIgnoreCase) && (i > ((productPage - 1) * PageSize)) && i <= ((productPage - 1) * PageSize) + PageSize) { listRecipe.Add(recipe); }
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
				if (recipe.Status.Equals("public", StringComparison.OrdinalIgnoreCase))
					listRecipe.Add(recipe);

			}
			// return _dbSet.Where(p => p.Title.Contains(keyword)).ToList();
			return listRecipe;
		}

		public List<Recipe> getPaingRecipe(int productPage, int PageSize, List<Recipe> list)
		{

			//var list = _dbSet.Where(Entity => Entity.Title.Contains(keyword)).ToList();
			List<Recipe> listRecipe = new List<Recipe>();


			int i = 1;
			foreach (Recipe recipe in list)
			{
				if (recipe.Status.Equals("public", StringComparison.OrdinalIgnoreCase) && i > ((productPage - 1) * PageSize) && i <= ((productPage - 1) * PageSize) + PageSize)
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

