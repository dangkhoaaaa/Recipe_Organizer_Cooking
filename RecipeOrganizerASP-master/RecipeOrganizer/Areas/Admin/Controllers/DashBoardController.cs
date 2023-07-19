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
            model.Recipes = _dashBroadRepository.Top5Recipe();
            model.recipeDrash = _dashBroadRepository.GetRecipebyDraft();
            model.recipeTrash =_dashBroadRepository.GetRecipebyTrash();
            model.NumberRecipeofMonth1 = _dashBroadRepository.NumberRecipeTime(1, 2023);
            model.NumberRecipeofMonth2 = _dashBroadRepository.NumberRecipeTime(2, 2023);
            model.NumberRecipeofMonth3 = _dashBroadRepository.NumberRecipeTime(3, 2023);
            model.NumberRecipeofMonth4 = _dashBroadRepository.NumberRecipeTime(4, 2023);
            model.NumberRecipeofMonth5 = _dashBroadRepository.NumberRecipeTime(5, 2023);
            model.NumberRecipeofMonth6 = _dashBroadRepository.NumberRecipeTime(6, 2023);
            model.NumberRecipeofMonth7 = _dashBroadRepository.NumberRecipeTime(7, 2023);
            model.OldUser1_10 = _dashBroadRepository.NumberOldUser(0, 10);
            model.OldUser10_20 = _dashBroadRepository.NumberOldUser(10, 20);
            model.OldUser20_30 = _dashBroadRepository.NumberOldUser(20, 30);
            model.OldUser30_40 = _dashBroadRepository.NumberOldUser(30, 40);
            model.OldUser40_50 = _dashBroadRepository.NumberOldUser(40, 50);
            model.OldUser50_60 = _dashBroadRepository.NumberOldUser(50,60);
            model.OldUser60_200 = _dashBroadRepository.NumberOldUser(60, 200);
            return View(model);
        }

    }
}
