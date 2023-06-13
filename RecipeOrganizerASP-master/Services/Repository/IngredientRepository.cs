using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
	public class IngredientRepository : RepositoryBase<Ingredient>
	{
		public void addIngredient(string IngredientsInput, int recipeId)
		{
			string[] ingredients = IngredientsInput.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var ingredientName in ingredients)
			{
				Ingredient ingredient = new Ingredient
				{
					RecipeId = recipeId,
					IngredientName = ingredientName
				};
				_dbSet.Add(ingredient);
				_context.SaveChanges();
			}
		}

		public List<Ingredient> GetByRecipeId(int recipeId)
		{
			return _dbSet.Where(i => i.RecipeId == recipeId).ToList();
		}

	}
}
