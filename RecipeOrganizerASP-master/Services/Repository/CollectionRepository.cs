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

		public int CountCollection(int recipeId)
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

		public void AddCollection(int recipeId, int userId)
		{
			Collection collection = new Collection
			{
				RecipeId = recipeId,
				UserId = userId.ToString()
			};

			_dbSet.Add(collection);
			_context.SaveChanges();
		}

		public void RemoveCollection(int recipeId, int userId)
		{
			Collection collection = _dbSet.FirstOrDefault(c => c.RecipeId == recipeId && c.UserId == userId.ToString());

			if (collection != null)
			{
				_dbSet.Remove(collection);
				_context.SaveChanges();
			}
		}

	}
}
