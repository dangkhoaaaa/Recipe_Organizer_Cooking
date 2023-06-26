using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using RecipeOrganizer.Areas.Admin.Models.RecipeManage;
using RecipeOrganizer.Areas.Data;
using Services.Models;
using Services.Models.Authentication;
using Services.Repository;
using System.Data;
using System.Text.RegularExpressions;

namespace RecipeOrganizer.Areas.Admin.Controllers
{
	[Authorize(Roles = RoleName.Administrator)]
	[Area("Admin")]
	[Route("/Recipe/[action]")]
	public class RecipeController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		private readonly ILogger<ManageController> _logger;

		private UserRepository _userRepository;
		private RecipeRepository _recipeRepository;
		private MetadataRepository _metadataRepository;
		private readonly IngredientRepository _ingredientRepository;
		private readonly DirectionRepository _directionRepository;
		private readonly TagRepository _tagRepository;
		private readonly RecipeHasTagRepository _recipeHasTagRepository;

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
			_metadataRepository = new MetadataRepository();
			_ingredientRepository = new IngredientRepository();
			_directionRepository = new DirectionRepository();
			_tagRepository = new TagRepository();
			_recipeHasTagRepository = new RecipeHasTagRepository();
		}

		[HttpGet("/manage/recipe")]
		public async Task<IActionResult> Index()
		{
			var model = _recipeRepository.GetRecipesWithMetadata();
			return View(model);
		}

		public async Task<IActionResult> SearchRecipe(string keyword)
		{
			ViewBag.RecipeKeyword = keyword;
			var model = _recipeRepository.GetRecipesWithMetadata();
			if (keyword == null)
			{
                    ViewBag.NotFind = "";
					return View("Index", model);

            }
            var listSearchRecipe = model.Where(p => p.RecipeTitle.Contains(keyword.Trim()) || p.UserName.Contains(keyword.Trim())).ToList();
            if (listSearchRecipe.Count == 0)
            {

                ViewBag.NotFind = "No result match the keyword";
                return View("Index", listSearchRecipe);

            }
            return View("Index", listSearchRecipe);
        }

        // GET: /Recipe/Censhorship
        public async Task<IActionResult> PendingRecipe()
        {
            var model = _recipeRepository.GetPendingRecipesWithMetadata();
            return View(model);
        }

        [HttpPost]
        public ActionResult PendingRecipe(int recipeID, string Status)
        {
			_recipeRepository.UpdateApprovalStatus(recipeID, Status);
            return RedirectToAction(nameof(PendingRecipe));
        }

        // GET: /Recipe/RecipeDetails
        public async Task<IActionResult> RecipeDetails(int recipeID)
		{
			var model = _recipeRepository.GetRecipesWithID(recipeID);
			model.AvgRate = 76;
			model.Ingredients = _ingredientRepository.GetByRecipeId(recipeID);
			model.Directions = _directionRepository.GetByRecipeId(recipeID);
			model.Tags = _recipeHasTagRepository.GetTagsByRecipeId(recipeID);
			return View(model);
		}




		//public List<RecipeViewModel> GetRecipesWithMetadata()
		//{
		//    var query = from m in _metadataRepository._dbSet
		//                join r in _recipeRepository._dbSet on m.RecipeId equals r.RecipeId
		//                join u in _userRepository._dbSet on m.UserId equals u.Id
		//                select new RecipeViewModel
		//                {
		//                    UserId = u.Id,
		//                    RecipeId = r.RecipeId,
		//                    UserName = u.UserName,
		//                    RecipeTitle = r.Title,
		//                    RecipeDescription = r.Description,
		//                    CreateDate = r.Date,
		//                    Status = r.Status,
		//                    NumberShare = r.NumberShare,
		//                    RecipeImage = r.Image
		//                };

		//    return query.ToList();
		//}
	}
}
