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
    public class DashboardController : Controller
    {

        private readonly CategoryRepository _categoryRepository;
        private readonly Parent_CategoryRepository _parentCategoryRepository;
        private readonly RecipeHasCategoryRepository _recipeHasCategory;
        private readonly DashboardRepository _dashBroadRepository;

        public DashboardController(UserManager<AppUser> userManager)
        {

            _parentCategoryRepository = new Parent_CategoryRepository();
            _categoryRepository = new CategoryRepository();
            _recipeHasCategory = new RecipeHasCategoryRepository();
            _dashBroadRepository = new DashboardRepository();
            

        }


        public IActionResult Index()
        {
         
            DashBroadModel model = new DashBroadModel();
            model.recipePending = _dashBroadRepository.GetRecipebyPending();
            model.recipePublic = _dashBroadRepository.GetRecipebyPublic();
            model.recipeReject =_dashBroadRepository.GetRecipebyReject();
            model.totalRecipe =_dashBroadRepository.TotalRecipe();
            model.totalView = _dashBroadRepository.TotalView();
            model.totalFeedback = _dashBroadRepository.TotalFeedback();
            model.totalAccount = _dashBroadRepository.TotalAccount();
            model.totalCategory = _dashBroadRepository.TotalCategiry();
            model.Recipes = _dashBroadRepository.Top4Recipe();
            return View(model);
        }

    }
}
