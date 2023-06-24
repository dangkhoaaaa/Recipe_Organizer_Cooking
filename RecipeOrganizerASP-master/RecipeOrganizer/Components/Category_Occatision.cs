using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Models.Authentication;
using Services.Repository;

namespace RecipeOrganizer.Components
{
	public class Category_Occatision : ViewComponent
	{
        
        private readonly CategoryRepository _categoryRepository;

        public Category_Occatision()
        {
            
            _categoryRepository = new CategoryRepository();

        }
        public IViewComponentResult Invoke(int productPage = 1)
        {
            // lay tat ca list recipe de dem so luong
            //List<Category> category21 = _categoryRepository.getListCategoryById(productPage);
            var categorys = _categoryRepository.getListCategoryById(3);
            return View(categorys);
        }
    }
}
