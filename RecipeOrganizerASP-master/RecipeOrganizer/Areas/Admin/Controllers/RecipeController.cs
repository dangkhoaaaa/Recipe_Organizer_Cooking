using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using RecipeOrganizer.Areas.Data;
using Services.Models.Authentication;
using Services.Repository;
using System.Data;

namespace RecipeOrganizer.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleName.Administrator)]
    [Area("Admin")]
    [Route("/Admin/[action]")]
    public class RecipeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly ILogger<ManageController> _logger;

        private UserRepository _userRepository;
        private RecipeRepository _recipeRepository;

        public RecipeController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IEmailSender emailSender,
        ILogger<ManageController> logger
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _recipeRepository = new RecipeRepository();
            _userRepository = new UserRepository();
        }

        [HttpGet("/manage/recipe")]
        public async Task<IActionResult> Index()
        {
            var listUser = _userRepository.GetAll();
            List<IndexViewModel> list = new List<IndexViewModel>();
            //get all user
            foreach (var user in listUser)
            {
                var role = await _userManager.GetRolesAsync(user);
                var isLockout = await _userManager.IsLockedOutAsync(user);
                var model = new IndexViewModel
                {
                    Member = user,
                    Role = role.ToList(),
                    TotalRecipe = _recipeRepository.GetByAuthor(user.Id).Count,
                    //TotalRecipe = 2,
                    Status = !isLockout
                };
                list.Add(model);
            }
            return View(list);
        }
    }
}
