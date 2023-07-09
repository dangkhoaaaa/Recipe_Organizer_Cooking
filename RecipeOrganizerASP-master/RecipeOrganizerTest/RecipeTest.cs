using NUnit.Framework;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Services.Models.Authentication;
using Services.Repository;
using Services.Models;
using Microsoft.EntityFrameworkCore;

namespace RecipeOrganizerTest
{
	[TestFixture]
	public class RecipeTest
	{
		private RecipeRepository _recipeRepository;
		private CategoryRepository _categoryRepository;

		[SetUp]
		public void Setup()
		{
			_recipeRepository = new RecipeRepository();
			_categoryRepository = new CategoryRepository();
		}

		[TestCase(1)]
		[TestCase(10)]
		[TestCase(105)]
		public void TestGetAllNumberCategories(int number)
		{
			// Arrange
			int expectedNumberOfCategories = number; // Assume there are 10 categories in the database.

			// Act
			List<Category> categories = _categoryRepository.GetAll();

			// Assert
			Assert.AreEqual(expectedNumberOfCategories, categories.Count);
		}


		[TestCase("Chickken","This is chickken")]
		[TestCase(null, "This is chickken")]
		public void TestAddCategorySuccess(string Category, string Description)
		{
			// Get the current number of categories.
			List<Category> categories = _categoryRepository.GetAll();
			int beforeNumberOfRecipes = categories.Count;

			// Add a new category.
			Category newCategory = new Category()
			{
				
				ParentId = 1,
				Title = Category,
				Description = Description,
				Image = "category.jpg"
			};
			_categoryRepository.Add(newCategory);

			// Get the updated number of categories.
			categories = _categoryRepository.GetAll();
			int afterNumberOfRecipes = categories.Count;

			// Assert that the number of categories has increased by one.
			Assert.AreEqual(beforeNumberOfRecipes + 1, afterNumberOfRecipes);
		}

	

		[TestCase("a")]
		[TestCase("Dogewgqwgweg312ddsw")]
		[TestCase("")]
		[TestCase(null)]
		public void TestSearchKeyWordMethodReturnValueSearch(string keyword)
		{
			

			// Act
			List<Recipe> recipes = _recipeRepository.SearchAllTitleWithFilter(string.Empty, keyword);

			// Assert
			foreach (Recipe recipe in recipes)
			{
				Assert.IsTrue(recipe.Title.ToLower().Contains(keyword.ToLower()));
			}
		}




	}
}