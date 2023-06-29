using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipeOrganizer.Areas.Admin.Models;
using RecipeOrganizer.Areas.Data;
using Services.Models.Authentication;
using Services.Repository;
using System.Data;

namespace RecipeOrganizer.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleName.Administrator)]
    [Area("Admin")]
    [Route("/DashBoard/[action]")]
    public class DashBoardController : Controller
    {

        private readonly CategoryRepository _categoryRepository;
        private readonly Parent_CategoryRepository _parentCategoryRepository;
        private readonly RecipeHasCategoryRepository _recipeHasCategory;

        public DashBoardController(UserManager<AppUser> userManager)
        {

            _parentCategoryRepository = new Parent_CategoryRepository();
            _categoryRepository = new CategoryRepository();
            _recipeHasCategory = new RecipeHasCategoryRepository();

        }


        public IActionResult Index()
        {
         
            return View();
        }

    }
}
