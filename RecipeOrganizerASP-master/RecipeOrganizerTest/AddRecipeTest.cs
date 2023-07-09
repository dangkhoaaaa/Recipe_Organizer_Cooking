
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
		private  RecipeRepository _recipeRepository;
		private  IngredientRepository _ingredientRepository;
		private  DirectionRepository _directionRepository;
		private  RecipeHasTagRepository _recipeHasTagRepository;
		private  TagRepository _tagRepository;
		private  RecipeHasCategoryRepository _recipeHasCategoryRepository;
		private  MetadataRepository _metadataRepository;
		private  MediaRepository _mediaRepository;
		private  CategoryRepository _categoryRepository;
		private  UserManager<AppUser> _userManager;
		private  CollectionRepository _collectionRepository;

		
        [SetUp]
        public void SetUp()
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
            _collectionRepository = new CollectionRepository();
        }


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
		//[T]
		[Test]
		// attribute indicates a method is a test method.
		public void TestAddRecipeSuccess()
		{
			// Lấy danh sách tất cả các công thức hiện có.
			List<Recipe> recipes = _recipeRepository.getAllRecipe();

			// Đếm số lượng công thức hiện có.
			int beforeNumberOfRecipes = recipes.Count;

			// Thêm một công thức mới vào repository.
			_recipeRepository.Add(new Recipe
			{
				Title = "lofen",
				Date = DateTime.Now,
				Description = "This is",
				Status = "pending"
				
			});

			// Lấy danh sách tất cả các công thức sau khi đã thêm công thức mới.
			List<Recipe> updatedRecipes = _recipeRepository.getAllRecipe();

			// Đếm số lượng công thức sau khi đã thêm công thức mới.
			int afterNumberOfRecipes = updatedRecipes.Count;

			// Kiểm tra xem số lượng công thức đã tăng lên chính xác một đơn vị hay không, sử dụng phương thức Assert.AreEqual().
			Assert.AreEqual(beforeNumberOfRecipes, afterNumberOfRecipes);
		}

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
