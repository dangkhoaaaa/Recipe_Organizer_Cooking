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

		//public void UpdateIngredients(string ingredientsInput, int recipeId)
		//{
		//	var existingIngredients = _dbSet.Where(i => i.RecipeId == recipeId).ToList();
		//	var existingIngredientsDict = existingIngredients.ToDictionary(i => i.IngredientName);

		//	string[] ingredients = ingredientsInput.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

		//	foreach (var ingredientName in ingredients)
		//	{
		//		if (existingIngredientsDict.ContainsKey(ingredientName))
		//		{
		//			existingIngredientsDict.Remove(ingredientName);
		//		}
		//		else
		//		{
		//			Ingredient ingredient = new Ingredient
		//			{
		//				RecipeId = recipeId,
		//				IngredientName = ingredientName
		//			};
		//			_dbSet.Add(ingredient);
		//		}
		//	}

		//	foreach (var ingredientToRemove in existingIngredientsDict.Values)
		//	{
		//		_dbSet.Remove(ingredientToRemove);
		//	}
		//	_context.SaveChanges();
		//}

		public void UpdateIngredients(string ingredientsInput, int recipeId)
		{
			var existingIngredients = _dbSet.Where(i => i.RecipeId == recipeId);
			_dbSet.RemoveRange(existingIngredients);
			_context.SaveChanges();

			string[] ingredients = ingredientsInput.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var ingredientName in ingredients)
			{
				Ingredient ingredient = new Ingredient
				{
					RecipeId = recipeId,
					IngredientName = ingredientName
				};
				_dbSet.Add(ingredient);
			}
			_context.SaveChanges();
		}
	}
}
