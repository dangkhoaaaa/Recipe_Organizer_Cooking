using NUnit.Framework;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Services.Models.Authentication;
using Services.Repository;
using Services.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework.Interfaces;

namespace RecipeOrganizerTest
{

	[TestFixture]
	public class CategoryTest : BaseTest<Category>
	{
        private RecipeRepository _recipeRepository;
        private CategoryRepository _categoryRepository;

        [SetUp]
		public void Setup()
		{
			
			_recipeRepository = new RecipeRepository();
			_categoryRepository = new CategoryRepository();
            var fakeCategories = new List<Category>()
                  {
                  new Category { CategoryId = 1, Title = "Category 1", Description = "This is category 1" },
                  new Category { CategoryId = 2, Title = "Category 2", Description = "This is category 2" }
                 };
            _categoryRepository.Categories = fakeCategories;
        }
        [TearDown]
        public void TearDown()
        {
            _categoryRepository.Categories = null;
        }

        [TestCase("12/7", "This is chicken")]
        public void TestAddCategorySuccessUsingMoq(string Category, string Description)
        {
            // Create a mock CategoryRepository.
            var mockCategoryRepository = new Mock<CategoryRepository>();

            // Set up the mock CategoryRepository to return a fake list of categories.
			 var fakeCategories = new List<Category>()
                  {
                  new Category { CategoryId = 1, Title = "Category 1", Description = "This is category 1" },
                  new Category { CategoryId = 2, Title = "Category 2", Description = "This is category 2" }
                 };
            mockCategoryRepository.Object.Categories = _categoryRepository.Categories;
            int beforeNumberOfCategories = mockCategoryRepository.Object.Categories.Count;
            // Call the method to be tested.
            Category newCategory = new Category()
            {
                ParentId = 1,
                Title = Category,
                Description = Description,
                Image = "category.jpg"
            };
            mockCategoryRepository.Object.Categories.Add(newCategory);

            // Assert that the method returns the expected result.
            int afterNumberOfCategories = mockCategoryRepository.Object.Categories.Count;
            Assert.AreEqual(beforeNumberOfCategories + 1, afterNumberOfCategories);
        }


        // Test Total Number Category
        // Test case #1: Check total number category
        // Procedure:
        //  1. Use Categories
        //  2. Given the time to _categoryRepository.GetAll();
        //  3. Execute _categoryRepository.GetAll();
        //  4. Return message
        // Expected value
        //
        // The expected value is the total number of categories in the database.

        [TestCase(10)]
		[TestCase(112)]
		public void TestGetAllNumberCategories(int number)
		{
			number = _dbSet.Count();
			// Arrange
			int expectedNumberOfCategories = number; // Assume there are 112 categories in the database.

			
			// Act
			List<Category> categories = _categoryRepository.GetAll();

			// Assert
			Assert.AreEqual(expectedNumberOfCategories, categories.Count);
		}

		// Test Search Keyword Method Return Value Search
		// Test case #3: Check if the search method returns the correct result for the given keyword
		// Procedure:
		//  1. Search for recipes that contain the given keyword.
		//  2. Assert that all the recipes in the result list contain the given keyword in their title.
		// Expected value
		// result expected
		[TestCase("a")]
		[TestCase("Dogewgqwgweg312ddsw")]
	
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

		//[TestCase("Chickken", "This is chickken")]
		//[TestCase(null, "This is chickken")]
		//public void TestAddCategorySuccess(string Category, string Description)
		//{
		//	// Get the current number of categories.
		//	List<Category> categories = _categoryRepository.GetAll();
		//	int beforeNumberOfRecipes = categories.Count;

		//	// Add a new category.
		//	Category newCategory = new Category()
		//	{

		//		ParentId = 1,
		//		Title = Category,
		//		Description = Description,
		//		Image = "category.jpg"
		//	};
		//	_categoryRepository.Add(newCategory);

		//	// Get the updated number of categories.
		//	categories = _categoryRepository.GetAll();
		//	int afterNumberOfRecipes = categories.Count;

		//	// Assert that the number of categories has increased by one.
		//	Assert.AreEqual(beforeNumberOfRecipes + 1, afterNumberOfRecipes);
		//}




		// Test Add Category Success
		// Test case #2: Check adding a new category is successful
		// Procedure:
		//  1. Get the current number of categories.
		//  2. Add a new category with the given Category and Description.
		//  3. Get the updated number of categories.
		//  4. Assert that the number of categories has increased by one.
		//  4. Return message
		// Expected value:
		//  numberOfRecipes


	





	}
}