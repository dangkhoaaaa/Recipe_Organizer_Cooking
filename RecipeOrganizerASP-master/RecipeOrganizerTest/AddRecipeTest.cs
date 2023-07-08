
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Utilities;
using Microsoft.AspNetCore.Identity;
using Services.Models.Authentication;
using Services.Repository;
using Services.Models;

namespace RecipeOrganizerTest
{
	[TestFixture]
	// attribute denotes a class that contains unit tests
	public class AddRecipeTest
	{
		private readonly RecipeRepository _recipeRepository;
		private readonly IngredientRepository _ingredientRepository;
		private readonly DirectionRepository _directionRepository;
		private readonly RecipeHasTagRepository _recipeHasTagRepository;
		private readonly TagRepository _tagRepository;
		private readonly RecipeHasCategoryRepository _recipeHasCategoryRepository;
		private readonly MetadataRepository _metadataRepository;
		private readonly MediaRepository _mediaRepository;
		private readonly CategoryRepository _categoryRepository;
		private readonly UserManager<AppUser> _userManager;
		private readonly CollectionRepository _collectionRepository;

		public AddRecipeTest(UserManager<AppUser> userManager)
		{
			_recipeRepository = new RecipeRepository();
			_ingredientRepository = new IngredientRepository();
			_directionRepository = new DirectionRepository();
			_recipeHasTagRepository = new RecipeHasTagRepository();
			_tagRepository = new TagRepository();
			_recipeHasCategoryRepository = new RecipeHasCategoryRepository();
			_metadataRepository = new MetadataRepository();
			_mediaRepository = new MediaRepository();
			_categoryRepository = new CategoryRepository();
			_userManager = userManager;
			_collectionRepository = new CollectionRepository();
		}


		//[Test]
		//// attribute indicates a method is a test method.
		//public void TestAddRecipeSuccess()
		//{
		//	// Lấy danh sách tất cả các công thức hiện có.
		//	List<Recipe> recipes = _recipeRepository.getAllRecipe();

		//	// Đếm số lượng công thức hiện có.
		//	int beforeNumberOfRecipes = recipes.Count;

		//	// Thêm một công thức mới vào repository.
		//	_recipeRepository.addRecipe(new Recipe());

		//	// Lấy danh sách tất cả các công thức sau khi đã thêm công thức mới.
		//	[Test]
		//	// attribute indicates a method is a test method.
		//	public void TestAddRecipeSuccess()
		//	{
		//		// Lấy danh sách tất cả các công thức hiện có.
		//		List<Recipe> recipes = _recipeRepository.getAllRecipe();

		//		// Đếm số lượng công thức hiện có.
		//		int beforeNumberOfRecipes = recipes.Count;

		//		// Thêm một công thức mới vào repository.
		//		_recipeRepository.addRecipe(new Recipe());

		//		// Lấy danh sách tất cả các công thức sau khi đã thêm công thức mới.
		//		List<Recipe> updatedRecipes = _recipeRepository.getAllRecipe();

		//		// Đếm số lượng công thức sau khi đã thêm công thức mới.
		//		int afterNumberOfRecipes = updatedRecipes.Count;

		//		// Kiểm tra xem số lượng công thức đã tăng lên chính xác một đơn vị hay không, sử dụng phương thức Assert.AreEqual().
		//		Assert.AreEqual(beforeNumberOfRecipes + 1, afterNumberOfRecipes);
		//	}
		//	List<Recipe> updatedRecipes = _recipeRepository.getAllRecipe();

		//	// Đếm số lượng công thức sau khi đã thêm công thức mới.
		//	int afterNumberOfRecipes = updatedRecipes.Count;

		//	// Kiểm tra xem số lượng công thức đã tăng lên chính xác một đơn vị hay không, sử dụng phương thức Assert.AreEqual().
		//	Assert.AreEqual(beforeNumberOfRecipes + 1, afterNumberOfRecipes);
		//}


		[Test]
		public void SearchKeyWordMethod()
		{
			// Arrange
			RecipeRepository repository = new RecipeRepository();
			string keyword = "chicken";

			// Act
			List<Recipe> recipes = repository.SearchAllTitleWithFilter(string.Empty, keyword);

			// Assert
			foreach (Recipe recipe in recipes)
			{
				Assert.IsTrue(recipe.Title.ToLower().Contains(keyword.ToLower()));
			}
		}

		[Test]
		public void SearchKeyWordMethodwithValueNull()
		{
			// Arrange
			RecipeRepository repository = new RecipeRepository();
			string keyword = null;

			// Act
			List<Recipe> recipes = repository.SearchAllTitleWithFilter(string.Empty,keyword);

			// Assert
			Assert.IsEmpty(recipes);
		}

		[Test]
		// attribute indicates a method is a test method.
		public void TestAddRecipeSuccess()
		{
			// Lấy danh sách tất cả các công thức hiện có.
			List<Recipe> recipes = _recipeRepository.getAllRecipe();

			// Đếm số lượng công thức hiện có.
			int beforeNumberOfRecipes = recipes.Count;

			// Thêm một công thức mới vào repository.
			_recipeRepository.Add(new Recipe());

			// Lấy danh sách tất cả các công thức sau khi đã thêm công thức mới.
			List<Recipe> updatedRecipes = _recipeRepository.getAllRecipe();

			// Đếm số lượng công thức sau khi đã thêm công thức mới.
			int afterNumberOfRecipes = updatedRecipes.Count;

			// Kiểm tra xem số lượng công thức đã tăng lên chính xác một đơn vị hay không, sử dụng phương thức Assert.AreEqual().
			Assert.AreEqual(beforeNumberOfRecipes + 1, afterNumberOfRecipes);
		}


		//	private CategoryRepository _categoryRepository;

		//[Test]
		//public void TestCategoryRecipe()
		//{
		//	// Arrange
		//	RecipeRepository repository = new RecipeRepository();
		//	string categoryName = "New category";
		//	int expectedCount = 3;

		//	// Tạo danh mục mới và thêm vào đó một số công thức.
		//	Category category = new Category(categoryName);
		//	Recipe recipe1 = new Recipe("Recipe 1", category);
		//	Recipe recipe2 = new Recipe("Recipe 2", category);
		//	Recipe recipe3 = new Recipe("Recipe 3", category);
		//	repository.addRecipe(recipe1);
		//	repository.addRecipe(recipe2);
		//	repository.addRecipe(recipe3);

		//	// Act
		//	List<Recipe> recipes = repository.GetRecipesByCategory(categoryName);

		//	// Assert
		//	Assert.AreEqual(expectedCount, recipes.Count);
		//}
		[Test]
		public void TestNumberOfCategories()
		{
			// Arrange
			int expectedNumberOfCategories = 5; // vo vong 

			// Act
			List<Category> categories = _categoryRepository.GetAllCategories();

			// Assert
			Assert.AreEqual(expectedNumberOfCategories, categories.Count);
		}


	}
}
