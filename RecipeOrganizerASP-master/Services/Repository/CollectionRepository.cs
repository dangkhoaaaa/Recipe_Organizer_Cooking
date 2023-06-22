using Microsoft.EntityFrameworkCore;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
	public class CollectionRepository : RepositoryBase<Collection>
	{
		RecipeRepository _recipeRepository;

		public CollectionRepository()
		{
			_recipeRepository = new RecipeRepository();
		}

		public List<Collection> GetCollections()
		{
			List<Collection> collections = _dbSet.ToList();

			return collections;
		}
		public bool IsRecipeSaved(int recipeId, int userId)
		{
			return _dbSet.Any(c => c.RecipeId == recipeId && c.UserId == userId.ToString());
		}

		public int CountRecipeCollection(int recipeId)
		{
			List<Collection> collections = GetCollections();
			Recipe recipe = _recipeRepository.GetById(recipeId);
			if (recipe != null)
			{
				int likeCount = collections.Count(c => c.RecipeId == recipeId);
				return likeCount;
			}
			return 0;
		}

		public void ToggleCollection(int recipeId, int userId)
		{
			Collection? collection = _dbSet.FirstOrDefault(c => c.RecipeId == recipeId && c.UserId == userId.ToString());

			if (collection != null)
			{
				_dbSet.Remove(collection);
			}
			else
			{
				collection = new Collection
				{
					RecipeId = recipeId,
					UserId = userId.ToString()
				};
				_dbSet.Add(collection);
			}

			_context.SaveChanges();
		}

	}
}
