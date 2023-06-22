using Microsoft.EntityFrameworkCore;
using Services.Models;
using Services.Models.Authentication;
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
		public bool IsRecipeSaved(int recipeId, string userId)
		{
			return _dbSet.Any(c => c.RecipeId == recipeId && c.UserId == userId.ToString());
		}

		public List<bool> CollectionList(List<Recipe> recipes, string userId)
		{
			List<bool> results = new List<bool>();

			CollectionRepository collectionRepository = new CollectionRepository();

			List<int> recipeId = recipes.Select(r => r.RecipeId).ToList();
			foreach (int id in recipeId)
			{
				bool isSaved = collectionRepository.IsRecipeSaved(id, userId);
				results.Add(isSaved);
			}
			return results;
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

		public void ToggleCollection(int recipeId, string userId)
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
