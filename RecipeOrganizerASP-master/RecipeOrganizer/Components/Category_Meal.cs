using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Models.Authentication;
using Services.Repository;

namespace RecipeOrganizer.Components
{
	public class Category_Meal : ViewComponent
	{
        private readonly RecipeRepository _recipeRepository;
        private readonly IngredientRepository _ingredientRepository;
        private readonly DirectionRepository _directionRepository;
        private readonly RecipeHasTagRepository _recipeHasTagRepository;
        private readonly TagRepository _tagRepository;
        private readonly RecipeHasCategoryRepository _recipeHasCategoryRepository;
        private readonly MetadataRepository _metadataRepository;
        private readonly MediaRepository _mediaRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly CategoryRepository _categoryRepository;

        public Category_Meal(UserManager<AppUser> userManager)
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

        }
        public IViewComponentResult Invoke(int productPage = 1)
        {
            // lay tat ca list recipe de dem so luong
            //List<Category> category21 = _categoryRepository.getListCategoryById(productPage);
            var categorys = _categoryRepository.getListCategoryById(2);
            return View(categorys);
        }
    }
}
