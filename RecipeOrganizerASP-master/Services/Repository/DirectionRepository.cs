using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
	public class DirectionRepository : RepositoryBase<Direction>
	{
		public void addDirection(string directions, int recipeId)
		{
			string[] steps = directions.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < steps.Length; i++)
			{
				Direction direction = new Direction
				{
					RecipeId = recipeId,
					Step = i + 1,
					Direction1 = steps[i]
				};
				_dbSet.Add(direction);
				_context.SaveChanges();
			}
		}

		public List<Direction> GetByRecipeId(int recipeId)
		{
			return _dbSet.Where(d => d.RecipeId == recipeId).ToList();
		}

	}
}
