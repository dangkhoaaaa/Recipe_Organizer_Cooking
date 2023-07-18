using Firebase.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Data;
using Services.Models;
using Services.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Services.Repository
{
	public class RecipeRepository : RepositoryBase<Recipe>
	{
        private new readonly Recipe_OrganizerContext _context;

		protected DbSet<Recipe> _dbSet;
		protected DbSet<RecipeHasCategory> _dbSet1;
		protected DbSet<AppUser> _dbSetUser;
		protected DbSet<Metadata> _dbSetMetadata;
        protected DbSet<Notification> _dbSetNotification;
		private NotificationRepository _notificationRepository;
        public RecipeRepository()
		{
            _notificationRepository = new NotificationRepository();
			_context = new Recipe_OrganizerContext();
			_dbSet = _context.Set<Recipe>();
			_dbSet1 = _context.Set<RecipeHasCategory>();
            _dbSetMetadata = _context.Set<Metadata>();
            _dbSetUser = _context.Set<AppUser>();
            _dbSetNotification = _context.Set<Notification>();
        }

		public ICollection<Recipe> Products { get; set; } = new List<Recipe>();

		public List<Recipe> getRecipeByKeyword(string keyword)
		{
			List<Recipe> listRecipe = new List<Recipe>();
		   //	if (keyword.Equals("")) return _dbSet.ToList();
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

		public Recipe? GetById(int id, string status)
		{
			return _dbSet.Where(r => r.RecipeId == id && r.Status.Equals(status)).FirstOrDefault();
		}
		public Recipe? GetByIdForEdit(int id)
		{
			var recipe = _dbSet.Where(r => r.RecipeId == id).FirstOrDefault();
			if (recipe != null && recipe.Status != "trash" && recipe.Status != "rejected")
			{
				return recipe;
			}
			return null;
		}

		public Recipe? GetRecipeByAuthor(int recipeId, string userId)
		{
			Recipe? recipe = null;
			if (!string.IsNullOrEmpty(userId) && recipeId != 0)
			{
				recipe = _dbSet.FirstOrDefault(r => r.MetaData.Any(md => md.UserId == userId
																		  && md.RecipeId == recipeId
																		  && md.FeedbackId == null));
			}
			return recipe;
		}
		public Recipe? GetRecipeByFeedBackId(int feedBackId, string userId)
		{
			Recipe? recipe = null;
			if (!string.IsNullOrEmpty(userId) && feedBackId != 0)
			{
				recipe = _dbSet.FirstOrDefault(r => r.MetaData.Any(md => md.UserId == userId && md.FeedbackId == feedBackId));
			}
			return recipe;
		}
		public List<Recipe> GetByAuthor(string userId)
		{
			List<Recipe> listRecipe = _dbSet.Where(r => r.MetaData.Any(md => md.UserId == userId && md.FeedbackId == null))
										   .ToList();

			return listRecipe;
		}
		public List<Recipe> GetByAuthorPending(string userId)
		{
			List<Recipe> listRecipe = GetByAuthor(userId);
			List<Recipe> pendingRecipes = new List<Recipe>();
			if (listRecipe != null)
			{
				foreach (var item in listRecipe)
				{
					Recipe? pendingRecipe = _dbSet.FirstOrDefault(r => r.RecipeId == item.RecipeId && r.Status.Equals("pending"));
					if (pendingRecipe != null)
					{
						pendingRecipes.Add(pendingRecipe);
					}
				}
			}
			return pendingRecipes;
		}

		public Recipe? GetRecipe(int recipeId) { 
			return GetAll().Where(p => p.RecipeId == recipeId).FirstOrDefault();
		}

		public bool ChangeStatusRecipe(int recipeId, string newStatus)
		{
			bool result = false;
			var existingRecipe = _context.Recipes.Local.FirstOrDefault(r => r.RecipeId == recipeId);
			if (existingRecipe != null)
			{
				existingRecipe.Status = newStatus;
				result = true;
			}
			else
			{
				var recipe = GetById(recipeId);
				if (recipe != null)
				{
					recipe.Status = newStatus;
					Update(recipe);
					result = true;
				}
			}
			return result;
		}

		public Recipe? GetById(int id)
		{
			return GetAll().Where(r => r.RecipeId == id).FirstOrDefault();
		}

		public void UpdateApprovalStatus(int recipeId, string action)
		{
			if (recipeId > 0 && action != null)
			{
				string status;
				if (action == "Public")
				{
					status = "public";
				}
				else if (action == "Rejected")
				{
					status = "rejected";
				}
				else
				{
					return;
				}
				ChangeStatusRecipe(recipeId, status);
			}
		}

		public void IncreaseNumberShare(int recipeId)
		{
			var recipe = _dbSet.FirstOrDefault(r => r.RecipeId == recipeId);
			if (recipe != null)
			{
				recipe.NumberShare += 1;
				_context.SaveChanges();
			}
		}

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

        //Support for meal plan
        public List<Recipe> SearchAllWithFilter(string filter)
        {
            switch (filter)
            {
                case "1":
                    return _dbSet.Where(r => r.Status.Equals("public")).OrderByDescending(r => r.NumberShare).ToList();
                case "2":
                    return _dbSet.Where(r => r.Status.Equals("public")).OrderBy(r => r.Date).ToList();
                case "3":
                    return _dbSet.Where(r => r.Status.Equals("public")).OrderBy(r => r.Title).ToList();
                case "4":
                    return _dbSet.Where(r => r.Status.Equals("public")).OrderBy(r => r.RecipeId).ToList();
                default:
                    return _dbSet.Where(r => r.Status.Equals("public")).OrderBy(r => r.Title).ToList();

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

		public List<Recipe> GetRecipesByTags(List<string> tags)
		{
			// Convert tags to lowercase for case-insensitive matching
			var lowercaseTags = tags.Select(tag => tag.ToLower()).ToList();

			var query = _context.Recipes
				.Join(
					_context.RecipeHasTags,
					recipe => recipe.RecipeId,
					recipeTag => recipeTag.RecipeId,
					(recipe, recipeTag) => new { Recipe = recipe, RecipeTag = recipeTag }
				)
				.Join(
					_context.Tags,
					recipeTag => recipeTag.RecipeTag.TagId,
					tag => tag.TagId,
					(recipeTag, tag) => new { Recipe = recipeTag.Recipe, Tag = tag }
				)
				.Where(rt => lowercaseTags.Contains(rt.Tag.TagName.ToLower()))
				.Select(rt => rt.Recipe)
				.Distinct()
				.ToList();

			return query;
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

		public List<RecipeViewModel> GetRecipesWithMetadata()
		{
			//var query = from m in _context.MetaData
			//			join r in _context.Recipes on m.RecipeId equals r.RecipeId
			//			join u in _context.Users on m.UserId equals u.Id
			//			//where r.Status.Equals("pending") && r.Status.Equals("draft")
			//			select new RecipeViewModel
			//			{
			//				UserId = u.Id,
			//				RecipeId = r.RecipeId,
			//				UserName = u.UserName,
			//				UserImage = u.Image,
			//				RecipeTitle = r.Title,
			//				RecipeDescription = r.Description,
			//				CreateDate = r.Date,
			//				Status = r.Status,
			//				NumberShare = r.NumberShare,
			//				RecipeImage = r.Image,
			//				AvgRate = r.AvgRate,
			//			};
			


            //var query2 = from u in _context.Users
            //             join m in _context.MetaData on u.Id equals m.UserId
            //             join r in _context.Recipes on m.RecipeId equals r.RecipeId
            //             group new { u.Id, u.UserName, r.RecipeId } by new { u.Id, u.UserName, r.RecipeId } into g
            //             orderby g.Key.RecipeId
            //             select new RecipeViewModel
            //             {
            //                 UserId = g.Key.Id,
            //                 UserName = g.Key.UserName,
            //                 RecipeId = g.Key.RecipeId
            //             };

			var query3 = from u in _context.Users
                         join m in _context.MetaData on u.Id equals m.UserId
                         join r in _context.Recipes on m.RecipeId equals r.RecipeId
                         group new { User = u, Recipe = r } by new { u.Id, r.RecipeId } into g
                         select new RecipeViewModel
                         {
                             UserId = g.Key.Id,
							 RecipeId = g.Key.RecipeId,
							 User = g.Select(x => x.User).FirstOrDefault(),
                             Recipe = g.Select(x => x.Recipe).FirstOrDefault()
                         };
            return query3.ToList();
		}

        public List<RecipeViewModel> GetRecipesWithMetadataOrderByDate()
        {
            var query3 = from u in _context.Users
                         join m in _context.MetaData on u.Id equals m.UserId
                         join r in _context.Recipes on m.RecipeId equals r.RecipeId
                         group new { User = u, Recipe = r } by new { u.Id, r.RecipeId } into g
                         select new RecipeViewModel
                         {
                             UserId = g.Key.Id,
                             RecipeId = g.Key.RecipeId,
                             User = g.Select(x => x.User).FirstOrDefault(),
                             Recipe = g.Select(x => x.Recipe).FirstOrDefault()
                         };
            return query3.ToList().OrderByDescending(x => x.Recipe.Date).ToList();
        }

        public List<RecipeViewModel> GetRecipesByStatusWithMetadata(string status)
		{
			//var query = from m in _context.MetaData
			//			join r in _context.Recipes on m.RecipeId equals r.RecipeId
			//			join u in _context.Users on m.UserId equals u.Id
			//			where r.Status.Equals(status) //&& r.Status.Equals("draft")
			//			select new RecipeViewModel
			//			{
			//				UserId = u.Id,
			//				RecipeId = r.RecipeId,
			//				UserName = u.UserName,
			//				UserImage = u.Image,
			//				RecipeTitle = r.Title,
			//				RecipeDescription = r.Description,
			//				CreateDate = r.Date,
			//				Status = r.Status,
			//				NumberShare = r.NumberShare,
			//				RecipeImage = r.Image,
			//				AvgRate = r.AvgRate,
			//			};

            var query2 = from m in _context.MetaData
                        join r in _context.Recipes on m.RecipeId equals r.RecipeId
                        join u in _context.Users on m.UserId equals u.Id
                        where r.Status.Equals(status) //&& r.Status.Equals("draft")
						group new { User = u, Recipe = r } by new { u.Id, r.RecipeId } into g
                         select new RecipeViewModel
                         {
                             UserId = g.Key.Id,
                             RecipeId = g.Key.RecipeId,
                             User = g.Select(x => x.User).FirstOrDefault(),
                             Recipe = g.Select(x => x.Recipe).FirstOrDefault()
                         };
            return query2.ToList();
		}

		public RecipeViewModel GetRecipesWithID(int recipeID)
		{
			//var query = from r in _context.Recipes
			//			where r.RecipeId == recipeID
			//			select new RecipeViewModel
			//			{
			//				RecipeId = r.RecipeId,
			//				RecipeTitle = r.Title,
			//				RecipeDescription = r.Description,
			//				CreateDate = r.Date,
			//				Status = r.Status,
			//				NumberShare = r.NumberShare,
			//				RecipeImage = r.Image,
			//				AvgRate = r.AvgRate,
			//			};
			var recipe = GetAll().Where(p => p.RecipeId == recipeID).FirstOrDefault();
			RecipeViewModel recipeViewModel = new RecipeViewModel {
				Recipe = recipe
			};
			return recipeViewModel;
		}

        public string ToStringCategory (Recipe recipe)
		{
			RecipeHasCategoryRepository recipeHasCategoryRepository = new RecipeHasCategoryRepository();
			CategoryRepository categoryRepository = new CategoryRepository();
			string categoryName = string.Empty;
			if (recipe != null)
			{
				List<string> categoryNames = new List<string>();
				var categorys = recipeHasCategoryRepository.GetCategoryByRecipeId(recipe.RecipeId);
				if ( categorys.Count > 0) {
					foreach ( var category in categorys ) {
						if ( categoryNames.Count() != 0)
						{
							var flag = true;
							foreach (var ca in categoryNames )
							{
								if (ca.Equals(categoryRepository.getInfCategory(category.CategoryId).Title))
								{
                                    flag = false;
                                    break;
                                }
								
							}
							if(flag)
							{
                                categoryNames.Add(categoryRepository.getInfCategory(category.CategoryId).Title);
                            }
						} else
						{
                            categoryNames.Add(categoryRepository.getInfCategory(category.CategoryId).Title);
                        }
						
					}

				}
				categoryName = string.Join(", ", categoryNames);
			}
			return categoryName;
		}

    }
}

